using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
    public class RadioChannelTests
    {
        private Radio _radioOff;
        private Radio _radioOn;
        private int _channel;

        [SetUp]
        public void Setup()
        {
            _radioOff = new Radio();
            _radioOff.TurnOff();

            _radioOn = new Radio();
            _radioOn.TurnOn();

            _channel = _radioOn.Channel;
        }

        [Test]
        public void ChannelCanOnlyDisplayWhenTheRadioIsOn()
        {
            Assert.AreEqual("Radio is off", _radioOff.Play());
            Assert.AreEqual($"Playing channel {_channel}", _radioOn.Play());
        }

        [Test]
        public void ChannelCanOnlyChangeWhenTheRadioIsOn()
        {
            _radioOff.SwitchToPreviousChannel();
            _radioOn.SwitchToNextChannel();

            Assert.AreEqual(1, _radioOff.Channel);
            Assert.AreEqual(2, _radioOn.Channel);
        }

        [Test]
        public void ChannelCanNotEscapeMinimumBoundaryValue()
        {
            _radioOn.SwitchToPreviousChannel();

            Assert.AreEqual(4, _radioOn.Channel);
        }

        [Test]
        public void ChannelCanNotEscapeMaximumBoundaryValue()
        {
            _radioOn.Channel = 4;
            _radioOn.SwitchToNextChannel();

            Assert.AreEqual(1, _radioOn.Channel);
        }

        [Test]
        public void ShuffleCanOnlyChangeWhenRadioIsOn()
        {
            _radioOff.ToggleShuffle();
            _radioOff.SwitchToNextChannel();

            _radioOn.ToggleShuffle();
            _radioOn.SwitchToNextChannel();

            Assert.AreEqual(1, _radioOff.Channel);
            Assert.AreNotEqual(1, _radioOn.Channel);
        }

        [Test]
        public void ShuffleWillAlwaysSwitchToAUniqueChannel()
        {
            _radioOn.ToggleShuffle();
            _radioOn.SwitchToNextChannel();

            Assert.AreNotEqual(1, _radioOn.Channel);
        }

        [Test]
        public void ShuffleDoesNotAffectDirectChannelSwitching()
        {
            _radioOn.ToggleShuffle();
            _radioOn.SwitchToChannel(3);

            Assert.AreEqual(3, _radioOn.Channel);
        }
    }
}
