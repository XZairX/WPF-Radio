using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
    [TestFixture]
    public class RadioChannelTests
    {
        private const int _channelDefaultValueOf1 = 1;
        private const int _channelMinValueOf1 = 1;
        private const int _channelMaxValueOf4 = 4;

        private Radio CreateRadioOff()
        {
            var radio = new Radio();
            radio.TurnOff();
            return radio;
        }

        private Radio CreateRadioOn()
        {
            var radio = new Radio();
            radio.TurnOn();
            return radio;
        }

        [Test]
        public void Channel_HasDefaultValueOf1()
        {
            var radio = new Radio();

            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [TestCase(_channelMinValueOf1)]
        [TestCase(_channelMaxValueOf4)]
        public void Channel_RadioIsOff_DoesNotSetChannel(int channel)
        {
            var radio = CreateRadioOff();

            radio.Channel = channel;
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [TestCase(_channelMinValueOf1)]
        [TestCase(_channelMaxValueOf4)]
        public void Channel_RadioIsOn_SetsValuesInsideChannelMinAndMaxBoundaries(int channel)
        {
            var radio = CreateRadioOn();
            
            radio.Channel = channel;
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(channel));
        }

        [TestCase(_channelMinValueOf1 - 1)]
        [TestCase(_channelMaxValueOf4 + 1)]
        public void Channel_RadioIsOn_DoesNotSetValuesOutsideChannelMinAndMaxBoundaries(int channel)
        {
            var radio = CreateRadioOn();

            radio.Channel = channel;
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [Test]
        public void Play_RadioIsOff_ReturnsRadioOffString()
        {
            var radio = CreateRadioOff();
            
            var result = radio.Play();

            Assert.That(result, Is.EqualTo("Radio is off"));
        }

        [Test]
        public void Play_RadioIsOn_ReturnsRadioOnString()
        {
            var radio = CreateRadioOn();

            var result = radio.Play();

            Assert.That(result, Is.EqualTo($"Playing channel {_channelDefaultValueOf1}"));
        }

        [Test]
        public void ToggleShuffle_RadioIsOff_ReturnsFalse()
        {
            var radio = CreateRadioOff();

            var result = radio.ToggleShuffle();

            Assert.That(result, Is.False);
        }

        [Test]
        public void ToggleShuffle_RadioIsOff_CanNotSetChannel()
        {
            var radio = CreateRadioOff();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [Test]
        public void ToggleShuffle_RadioIsOn_ReturnsTrue()
        {
            var radio = CreateRadioOn();

            var result = radio.ToggleShuffle();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ToggleShuffle_RadioIsOn_SetsChannelToARandomChannel()
        {
            var radio = CreateRadioOn();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.Not.EqualTo(_channelDefaultValueOf1));
        }

        [Test]
        public void ToggleShuffle_ToggleShuffleReturnsTrue_DisablesShuffleFunctionality()
        {
            var radio = CreateRadioOn();
            radio.ToggleShuffle();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1 + 1));
        }

        [TestCase(_channelDefaultValueOf1)]
        [TestCase(_channelDefaultValueOf1)]
        public void SwitchToChannel_RadioIsOff_DoesNotSetChannelToArgumentChannel(int channel)
        {
            var radio = CreateRadioOff();

            radio.SwitchToChannel(channel);
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [TestCase(_channelMinValueOf1)]
        [TestCase(_channelMaxValueOf4)]
        public void SwitchToChannel_RadioIsOn_AlwaysSetsChannelToArgumentChannel(int channel)
        {
            var radio = CreateRadioOn();

            radio.ToggleShuffle();
            radio.SwitchToChannel(channel);
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(channel));
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOff_DoesNotSetChannel()
        {
            var radio = CreateRadioOff();

            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOn_DecrementsChannel()
        {
            var radio = CreateRadioOn();
            radio.Channel += 1;

            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToPreviousChannel_ChannelIsMinimumValue_SetsChannelToMaximumValue()
        {
            var radio = CreateRadioOn();
            radio.Channel = _channelMinValueOf1;

            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelMaxValueOf4));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOff_DoesNotSetChannel()
        {
            var radio = CreateRadioOff();

            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOn_IncrementsChannel()
        {
            var radio = CreateRadioOn();

            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelDefaultValueOf1 + 1));
        }

        [Test]
        public void SwitchToNextChannel_ChannelIsMaximumValue_SetsChannelToMinimumValue()
        {
            var radio = CreateRadioOn();
            radio.Channel = _channelMaxValueOf4;

            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(_channelMinValueOf1));
        }
    }
}
