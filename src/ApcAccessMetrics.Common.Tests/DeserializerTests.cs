using System;
using Xunit;
using ApcAccessMetrics.Common.DeSerializer;

namespace ApcAccessMetrics.Common.Tests
{
    public class DeserializerTests
    {
        [Fact]
        public void ColonKeyValueDeserializerTestDict()
        {
            var text = @"APC      : 001,036,0882
                            DATE     : 2020-05-27 20:37:07 -0500  
                            HOSTNAME : raspberrypi
                                 VERSION  : 3.14.14 (31 May 2016) debian
                            UPSNA  ME  : raspberrypi
                            CABLE    : USB Cable
                            DRIVER   : USB UPS Driver
                            UPSMODE  : Stand Alone
                            STARTTIME: 2020-05-27 20:24:40 -0500  
                            MODEL    :";
            var dict = new ColonKeyValueDeSerializer().Deserialize(text);
            Assert.NotNull(dict);
            Assert.True(dict.Count > 0);
        }
    }
}
