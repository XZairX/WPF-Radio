using System;

namespace RadioApp
{
    public class Radio
    {
        private int _channel = 1;
        public int Channel
        {
            get => _channel;
            set
            {
                if (_isOn)
                {
                    if (value >= _lowerBound && value <= _upperBound)
                    {
                        _channel = value;
                    }
                }
            }
        }

        private int _volume = 50;
        public int Volume
        {
            get => _volume;
            private set
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

        private const int _lowerBound = 1;
        private const int _upperBound = 4;
        private const int _minVolume = 0;
        private const int _maxVolume = 100;

        private readonly Random _random = new Random();

        private int _savedVolume;

        private bool _isOn;
        private bool _isMuted;
        private bool _canShuffle;

        public string Play()
        {
            return _isOn ? $"Playing channel {_channel}" : "Radio is off";
        }

        public void TurnOff() => _isOn = false;
        
        public void TurnOn() => _isOn = true;
        
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
                Channel = (Channel == _lowerBound) ?
                    Channel = _upperBound : Channel -= 1;
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
                Channel = (Channel == _upperBound) ?
                    Channel = _lowerBound : Channel += 1;
            }
        }

        private int ShuffleChannel()
        {
            if (_isOn)
            {
                int currentChannel = Channel;
                while (Channel == currentChannel)
                {
                    Channel = _random.Next(_lowerBound, _upperBound + 1);
                }
            }
            return Channel;
        }
    }
}
