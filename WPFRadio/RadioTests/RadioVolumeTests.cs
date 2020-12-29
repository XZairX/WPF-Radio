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
        public void Mute_PreventsVolumeFromBeingSet()
        {
            var radio = CreateRadioOn();

            radio.Mute();
            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void Mute_CalledTwice_DisablesMuteFunctionality()
        {
            var radio = CreateRadioOn();

            radio.Mute();
            radio.Mute();
            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(51));
        }

        [Test]
        public void VolumeMin_RadioIsOff_DoesNotSetVolume()
        {
            var radio = CreateRadioOff();

            radio.VolumeMin();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void VolumeMin_RadioIsOn_SetsVolumeToMinimumValue()
        {
            var radio = CreateRadioOn();

            radio.VolumeMin();
            var result = radio.Volume;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void VolumeDown_VolumeIsMinimumValue_DoesNotSetVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeMin();
            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void VolumeMax_RadioIsOff_DoesNotSetVolume()
        {
            var radio = CreateRadioOff();

            radio.VolumeMax();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(50));
        }

        [Test]
        public void VolumeMax_RadioIsOn_SetsVolumeToMaximumValue()
        {
            var radio = CreateRadioOn();

            radio.VolumeMax();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void VolumeUp_VolumeIsMaximumValue_DoesNotSetVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeMax();
            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(100));
        }
    }
}
