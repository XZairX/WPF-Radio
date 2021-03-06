﻿using System;

namespace RadioApp
{
    public class Radio
    {
        private const int _minChannel = 1;
        private const int _maxChannel = 4;
        private const int _minVolume = 0;
        private const int _maxVolume = 100;

        private readonly Random _random = new Random();

        private int _channel = 1;
        private int _volume = 50;
        private int _savedVolume;

        private bool _isOn;
        private bool _isMuted;
        private bool _canShuffle;

        public int Channel
        {
            get => _channel;
            set
            {
                if (_isOn)
                {
                    if (value >= _minChannel && value <= _maxChannel)
                    {
                        _channel = value;
                    }
                }
            }
        }

        public int Volume
        {
            get => _volume;
            set
            {
                if (_isOn)
                {
                    if (value >= _minVolume && value <= _maxVolume)
                    {
                        _volume = value;
                    }
                }
            }
        }

        public void TurnOff() => _isOn = false;

        public void TurnOn() => _isOn = true;

        public string Play()
        {
            return _isOn ? $"Playing channel {_channel}" : "Radio is off";
        }
        
        public bool ToggleShuffle()
        {
            if (_isOn)
            {
                _canShuffle = !_canShuffle;
            }
            return _canShuffle;
        }

        public void SwitchToChannel(int channel) => Channel = channel;
        
        public void SwitchToPreviousChannel()
        {           
            if (_canShuffle)
            {
                Channel = ShuffleChannel();
            }
            else
            {
                Channel = (Channel == _minChannel) ?
                    Channel = _maxChannel : Channel -= 1;
            }
        }

        public void SwitchToNextChannel()
        {
            if (_canShuffle)
            {
                Channel = ShuffleChannel();
            }
            else
            {
                Channel = (Channel == _maxChannel) ?
                    Channel = _minChannel : Channel += 1;
            }
        }

        private int ShuffleChannel()
        {
            if (_isOn)
            {
                int currentChannel = Channel;
                while (Channel == currentChannel)
                {
                    Channel = _random.Next(_minChannel, _maxChannel + 1);
                }
            }
            return Channel;
        }

        public void Mute()
        {
            if (_isMuted)
            {
                UnMute();
            }
            else
            {
                _savedVolume = Volume;
                Volume = 0;
                _isMuted = true;
            }
        }

        private void UnMute()
        {
            if (_isOn)
            {
                Volume = _savedVolume;
                _isMuted = false;
            }
        }

        public void VolumeDown()
        {
            if (_isMuted)
            {
                UnMute();
            }
            else
            {
                Volume--;
            }
        }

        public void VolumeUp()
        {
            if (_isMuted)
            {
                UnMute();
            }
            else
            {
                Volume++;
            }
        }

        public void VolumeMin()
        {
            if (_isMuted)
            {
                UnMute();
            }
            else
            {
                Volume = _minVolume;
            }
        }

        public void VolumeMax()
        {
            if (_isMuted)
            {
                UnMute();
            }
            else
            {
                Volume = _maxVolume;
            }
        }
    }
}
