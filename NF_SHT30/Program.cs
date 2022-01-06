using System;
using System.Diagnostics;
using System.Threading;

//新たに追加
using nanoFramework.Hardware.Esp32; //デバイス
using System.Device.I2c;            //I2C
using Iot.Device.Sht3x;             //センサー
//

namespace NF_SHT30
{
    public class Program
    {
        public static void Main()
        {
            //I2Cで使うPinを設定
            Configuration.SetPinFunction(26, DeviceFunction.I2C1_DATA);                 //SDAはGPIO26に割り当て
            Configuration.SetPinFunction(32, DeviceFunction.I2C1_CLOCK);                //SCLはGPIO32に割り当て

            //センサーのアドレスを設定
            I2cConnectionSettings settings = new I2cConnectionSettings(1, 0x44);        //SHT30のアドレスは0x44、I2Cポートは１

            //センサーオブジェクトの作成
            using I2cDevice device = I2cDevice.Create(settings);
            using Sht3x sensor = new(device);

            while (true)
            {
                //センサーデータの取得
                double tmp = sensor.Temperature.DegreesCelsius;                         //温度を摂氏で取得
                double hum = sensor.Humidity.Percent;                                   //湿度をパーセントで取得

                //デバッグウィンドウに出力
                Debug.Write("tmp : ");
                Debug.WriteLine(tmp.ToString("F2"));
                Debug.Write("hum : ");
                Debug.WriteLine(hum.ToString("F2"));
                Debug.WriteLine(" ");

                Thread.Sleep(10000);                                                    //10秒待機

            }
        }
    }
}
