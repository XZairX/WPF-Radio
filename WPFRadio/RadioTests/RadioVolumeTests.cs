using NUnit.Framework;
using RadioApp;

namespace RadioTests
{
    [TestFixture]
    public class RadioVolumeTests
    {
        private const int _volumeDefaultValueOf50 = 50;
        private const int _volumeMinValueOf0 = 0;
        private const int _volumeMaxValueOf100 = 100;

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
        public void Volume_HasDefaultValueOf50()
        {
            var radio = new Radio();

            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [TestCase(_volumeMinValueOf0)]
        [TestCase(_volumeMaxValueOf100)]
        public void Volume_RadioIsOff_PreventsVolumeFromBeingSet(int volume)
        {
            var radio = CreateRadioOff();

            radio.Volume = volume;
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [TestCase(_volumeMinValueOf0)]
        [TestCase(_volumeMaxValueOf100)]
        public void Volume_RadioIsOn_SetsVolumeToValuesInsideMinMaxVolumeRange(int volume)
        {
            var radio = CreateRadioOn();

            radio.Volume = volume;
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(volume));
        }

        [TestCase(_volumeMinValueOf0 - 1)]
        [TestCase(_volumeMaxValueOf100 + 1)]
        public void Volume_RadioIsOn_DoesNotSetVolumeToValuesOutsideMinMaxVolumeRange(int volume)
        {
            var radio = CreateRadioOn();

            radio.Volume = volume;
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void Mute_SetsVolumeTo0()
        {
            var radio = CreateRadioOn();

            radio.Mute();
            var result = radio.Volume;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Mute_RadioIsMuted_SetsVolumeToVolumeBeforeMute()
        {
            var radio = CreateRadioOn();
            radio.Mute();

            radio.Mute();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void VolumeDown_RadioIsOn_DecrementsVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50 - 1));
        }

        [Test]
        public void VolumeDown_RadioIsMuted_SetsVolumeToVolumeBeforeMute()
        {
            var radio = CreateRadioOn();
            radio.Mute();

            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void VolumeUp_RadioIsOn_IncrementsVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50 + 1));
        }

        [Test]
        public void VolumeUp_RadioIsMuted_SetsVolumeToVolumeBeforeMute()
        {
            var radio = CreateRadioOn();
            radio.Mute();

            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void VolumeMin_RadioIsOn_SetsVolumeToMinimumVolume()
        {
            var radio = CreateRadioOn();

            radio.VolumeMin();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeMinValueOf0));
        }

        [Test]
        public void VolumeMin_RadioIsMuted_SetsVolumeToVolumeBeforeMute()
        {
            var radio = CreateRadioOn();
            radio.Mute();

            radio.VolumeMin();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void VolumeDown_VolumeIsMinimumValue_DoesNotSetVolume()
        {
            var radio = CreateRadioOn();
            radio.VolumeMin();

            radio.VolumeDown();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeMinValueOf0));
        }

        [Test]
        public void VolumeMax_RadioIsOff_DoesNotSetVolume()
        {
            var radio = CreateRadioOff();

            radio.VolumeMax();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeDefaultValueOf50));
        }

        [Test]
        public void VolumeMax_RadioIsOn_SetsVolumeToMaximumValue()
        {
            var radio = CreateRadioOn();

            radio.VolumeMax();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeMaxValueOf100));
        }

        [Test]
        public void VolumeUp_VolumeIsMaximumValue_DoesNotSetVolume()
        {
            var radio = CreateRadioOn();
            radio.VolumeMax();

            radio.VolumeUp();
            var result = radio.Volume;

            Assert.That(result, Is.EqualTo(_volumeMaxValueOf100));
        }
    }
}
