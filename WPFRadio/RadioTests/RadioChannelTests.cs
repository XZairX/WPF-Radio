using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
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
        public void SwitchToPreviousChannel_RadioIsOff_ChannelDoesNotChange()
        {
            var radio = CreateRadioOff();

            radio.SwitchToPreviousChannel();

            Assert.That(radio.Channel, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOn_ChannelDecrements()
        {
            var radio = CreateRadioOn();

            radio.Channel = 2;
            radio.SwitchToPreviousChannel();

            Assert.That(radio.Channel, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToPreviousChannel_ChannelIsMinimumValue_ChannelIsSetToMaximumValue()
        {
            var radio = CreateRadioOn();

            radio.SwitchToPreviousChannel();

            Assert.That(radio.Channel, Is.EqualTo(4));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOff_ChannelDoesNotChange()
        {
            var radio = CreateRadioOff();

            radio.SwitchToNextChannel();

            Assert.That(radio.Channel, Is.EqualTo(1));
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOn_ChannelIncrements()
        {
            var radio = CreateRadioOn();

            radio.SwitchToNextChannel();

            Assert.That(radio.Channel, Is.EqualTo(2));
        }

        [Test]
        public void SwitchToNextChannel_ChannelIsMaximumValue_ChannelIsSetToMinimumValue()
        {
            var radio = CreateRadioOn();

            radio.Channel = 4;
            radio.SwitchToNextChannel();

            Assert.That(radio.Channel, Is.EqualTo(1));
        }

        [Test]
        public void ToggleShuffle_RadioIsOff_ShuffleCommandCanNotBeToggled()
        {
            var radio = CreateRadioOff();

            var result = radio.ToggleShuffle();

            Assert.That(result, Is.False);
        }

        [Test]
        public void ToggleShuffle_RadioIsOn_ShuffleCommandCanBeToggled()
        {
            var radio = CreateRadioOn();

            var result = radio.ToggleShuffle();

            Assert.That(result, Is.True);
        }

        [Test]
        public void ToggleShuffle_RadioIsOff_ChannelCanNotChange()
        {
            var radio = CreateRadioOff();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();

            Assert.That(radio.Channel, Is.EqualTo(1));
        }

        [Test]
        public void ToggleShuffle_RadioIsOn_ChannelChangesToAUniqueChannel()
        {
            var radio = CreateRadioOn();

            radio.ToggleShuffle();
            radio.SwitchToNextChannel();

            Assert.That(radio.Channel, Is.Not.EqualTo(1));
        }

        [Test]
        public void SwitchToChannel_RadioIsOnWithToggleShuffleOn_ChannelIsSwitchedToArgumentChannel()
        {
            var radio = CreateRadioOn();

            radio.ToggleShuffle();
            radio.SwitchToChannel(3);

            Assert.That(radio.Channel, Is.EqualTo(3));
        }
    }
}
