using System;
using System.Diagnostics;
using System.Threading;

//�V���ɒǉ�
using nanoFramework.Hardware.Esp32; //�f�o�C�X
using System.Device.I2c;            //I2C
using Iot.Device.Sht3x;             //�Z���T�[
//

namespace NF_SHT30
{
    public class Program
    {
        public static void Main()
        {
            //I2C�Ŏg��Pin��ݒ�
            Configuration.SetPinFunction(26, DeviceFunction.I2C1_DATA);                 //SDA��GPIO26�Ɋ��蓖��
            Configuration.SetPinFunction(32, DeviceFunction.I2C1_CLOCK);                //SCL��GPIO32�Ɋ��蓖��

            //�Z���T�[�̃A�h���X��ݒ�
            I2cConnectionSettings settings = new I2cConnectionSettings(1, 0x44);        //SHT30�̃A�h���X��0x44�AI2C�|�[�g�͂P

            //�Z���T�[�I�u�W�F�N�g�̍쐬
            using I2cDevice device = I2cDevice.Create(settings);
            using Sht3x sensor = new(device);

            while (true)
            {
                //�Z���T�[�f�[�^�̎擾
                double tmp = sensor.Temperature.DegreesCelsius;                         //���x��ێ��Ŏ擾
                double hum = sensor.Humidity.Percent;                                   //���x���p�[�Z���g�Ŏ擾

                //�f�o�b�O�E�B���h�E�ɏo��
                Debug.Write("tmp : ");
                Debug.WriteLine(tmp.ToString("F2"));
                Debug.Write("hum : ");
                Debug.WriteLine(hum.ToString("F2"));
                Debug.WriteLine(" ");

                Thread.Sleep(10000);                                                    //10�b�ҋ@

            }
        }
    }
}
