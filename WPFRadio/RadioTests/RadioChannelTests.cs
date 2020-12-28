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
            
            radio.Play();

            Assert.AreEqual("Radio is off", radio.Play());
        }

        [Test]
        public void Play_RadioIsOn_ReturnsPlayingChannelString()
        {
            var radio = CreateRadioOn();

            radio.Play();

            Assert.AreEqual("Playing channel 1", radio.Play());
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOff_ChannelDoesNotChange()
        {
            var radio = CreateRadioOff();

            radio.SwitchToPreviousChannel();

            Assert.AreEqual(1, radio.Channel);    
        }

        [Test]
        public void SwitchToPreviousChannel_RadioIsOn_ChannelDecrements()
        {
            var radio = CreateRadioOn();

            radio.Channel = 2;
            radio.SwitchToPreviousChannel();

            Assert.AreEqual(1, radio.Channel);
        }

        [Test]
        public void SwitchToPreviousChannel_ChannelIsMinimumValue_ChannelIsSetToMaximumValue()
        {
            var radio = CreateRadioOn();

            radio.SwitchToPreviousChannel();

            Assert.AreEqual(4, radio.Channel);
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOff_ChannelDoesNotChange()
        {
            var radio = CreateRadioOff();

            radio.SwitchToNextChannel();

            Assert.AreEqual(1, radio.Channel);
        }

        [Test]
        public void SwitchToNextChannel_RadioIsOn_ChannelIncrements()
        {
            var radio = CreateRadioOn();

            radio.SwitchToNextChannel();

            Assert.AreEqual(2, radio.Channel);
        }

        [Test]
        public void SwitchToNextChannel_ChannelIsMaximumValue_ChannelIsSetToMinimumValue()
        {
            var radio = CreateRadioOn();

            radio.Channel = 4;
            radio.SwitchToNextChannel();

            Assert.AreEqual(1, radio.Channel);
        }

        [Test]
        public void ToggleShuffle_RadioIsOff_ChannelCanNotChange()
        {
            var radio = CreateRadioOff();

            radio.ToggleShuffle();

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
