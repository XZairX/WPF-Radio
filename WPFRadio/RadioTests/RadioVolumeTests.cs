using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
    [TestFixture]
    public class RadioVolumeTests
    {
        private Radio _radioOff;
        private Radio _radioOn;
        private int _volume;

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

        [SetUp]
        public void Setup()
        {
            _radioOff = new Radio();
            _radioOff.TurnOff();

            _radioOn = new Radio();
            _radioOn.TurnOn();

            _volume = _radioOn.Volume;
        }

        [Test]
        public void Volume_HasDefaultValueOf50()
        {
            var radio = new Radio();

            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void VolumeDown_RadioIsOff_DoesNotSetVolume()
        {
            var radio = CreateRadioOff();

            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void VolumeDown_RadioIsOn_DecrementsVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(49));
        }

        [Test]
        public void VolumeUp_RadioIsOff_DoesNotSetVolume()
        {
            var radio = CreateRadioOff();

            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void VolumeUp_RadioIsOn_IncrementsVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(51));
        }

        [Test]
        public void VolumeCanNotChangeWhenMute()
        {
            _radioOff.Mute();
            _radioOff.VolumeDown();

            _radioOn.Mute();
            _radioOn.VolumeUp();

            Assert.AreEqual(_volume, _radioOff.Volume);
            Assert.AreEqual(_volume, _radioOn.Volume);
        }

        [Test]
        public void VolumeCanNotEscapeMinimumBoundaryValue()
        {
            _radioOn.VolumeMin();
            _radioOn.VolumeDown();

            Assert.AreEqual(0, _radioOn.Volume);
        }

        [Test]
        public void VolumeCanNotEscapeMaximumBoundaryValue()
        {
            _radioOn.VolumeMax();
            _radioOn.VolumeUp();

            Assert.AreEqual(100, _radioOn.Volume);
        }
    }
}
