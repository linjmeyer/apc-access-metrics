using Xunit;
using ApcAccessMetrics.Common.Deserializer;
using ApcAccessMetrcs.Common.Apc;
using System;

namespace ApcAccessMetrics.Common.Tests
{
    public class DeserializerTests
    {
        [Fact]
        public void ColonKeyValueDeserializerTestDict()
        {
            var text = @"aAPC      : 001,036,0882
                            DATE     : 2020-05-27 20:37:07 -0500  
                            HOSTNAME : raspberrypi
                                 VERSION  : 3.14.14 (31 May 2016) debian
                            UPSNA  ME  : raspberrypi
                            CABLE    : USB Cable
                            DRIVER   : USB UPS Driver
                            UPSMODE  : Stand Alone
                            STARTTIME: 2020-05-27 20:24:40 -0500  
                            MODEL    :";
            var dict = new ColonKeyValueDeserializer().Deserialize(text);
            Assert.NotNull(dict);
            Assert.True(dict.Count > 0);
        }

        [Fact]
        public void ColonKeyValueDeserializerTestModelClass()
        {
            var text = @"   APC      : 001,036,0882
                            DATE     : 2020-05-28 20:51:42 -0500
                            HOSTNAME : raspberrypi
                            VERSION  : 3.14.14 (31 May 2016) debian
                            UPSNAME  : raspberrypi
                            CABLE    : USB Cable
                            DRIVER   : USB UPS Driver
                            UPSMODE  : Stand Alone
                            STARTTIME: 2020-05-27 20:24:40 -0500
                            MODEL    : Back-UPS ES 850G2
                            STATUS   : ONLINE
                            LINEV    : 123.0
                            LOADPCT  : 0.0
                            BCHARGE  : 100.0
                            TIMELEFT : 343.3
                            MBATTCHG : 5
                            MINTIMEL : 3
                            MAXTIME  : 0
                            SENSE    : Medium
                            LOTRANS  : 92.0
                            HITRANS  : 139.0
                            ALARMDEL : 30
                            BATTV    : 13.5
                            LASTXFER : Unacceptable line voltage changes
                            NUMXFERS : 0
                            TONBATT  : 0
                            CUMONBATT: 0
                            XOFFBATT : N/A
                            SELFTEST : NO
                            STATFLAG : 0x05000008
                            SERIALNO : 4B2005P22814
                            BATTDATE : 2020-01-30
                            NOMINV   : 120
                            NOMBATTV : 12.0
                            NOMPOWER : 450
                            FIRMWARE : 931.a10.D USB FW:a1
                            END APC  : 2020-05-28 20:51:56 -0500";
            var apcStatus = new ColonKeyValueDeserializer().Deserialize<ApcStatus>(text);
            Assert.False(apcStatus.BatteryChargeLevel <= 0);
            Assert.False(apcStatus.TimeLeft <= 0);
            Assert.False(String.IsNullOrWhiteSpace(apcStatus.Model));
            Assert.False(String.IsNullOrWhiteSpace(apcStatus.Name));
        }
    }
}
