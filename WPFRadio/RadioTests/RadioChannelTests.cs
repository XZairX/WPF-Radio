using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
    [TestFixture]
    public class RadioChannelTests
    {
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

            Assert.That(result, Is.EqualTo(1));
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

            Assert.That(result, Is.EqualTo("Playing channel 1"));
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOff_DoesNotSetChannel()
        {
            var radio = CreateRadioOff();

            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOn_DecrementsChannel()
        {
            var radio = CreateRadioOn();

            radio.Channel = 2;
            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToPreviousChannel_ChannelIsMinimumValue_SetsChannelToMaximumValue()
        {
            var radio = CreateRadioOn();

            radio.SwitchToPreviousChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOff_DoesNotSetChannel()
        {
            var radio = CreateRadioOff();

            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOn_IncrementsChannel()
        {
            var radio = CreateRadioOn();

            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SwitchToNextChannel_ChannelIsMaximumValue_SetsChannelToMinimumValue()
        {
            var radio = CreateRadioOn();

            radio.Channel = 4;
            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(1));
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

            Assert.That(result, Is.EqualTo(1));
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

            Assert.That(result, Is.Not.EqualTo(1));
        }

        [Test]
        public void ToggleShuffle_ToggleShuffleReturnsTrue_DisablesShuffleFunctionality()
        {
            var radio = CreateRadioOn();
            radio.ToggleShuffle();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(2));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void SwitchToChannel_RadioIsOff_DoesNotSetChannelToArgumentChannel(int channel)
        {
            var radio = CreateRadioOff();

            radio.SwitchToChannel(channel);
            var result = radio.Channel;

            Assert.That(result, Is.Not.EqualTo(channel));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void SwitchToChannel_RadioIsOn_AlwaysSetsChannelToArgumentChannel(int channel)
        {
            var radio = CreateRadioOn();

            radio.ToggleShuffle();
            radio.SwitchToChannel(channel);
            var result = radio.Channel;

            Assert.That(result, Is.EqualTo(channel));
        }
    }
}
