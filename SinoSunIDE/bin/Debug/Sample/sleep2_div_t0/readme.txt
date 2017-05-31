
本项目只供测试使用测试SLEEP功能

1、本项目WDT无分频，T0时钟由RTC提供
2、T0分频为256
3、测试SLEEP下RTC功耗时通过选项把WDT关闭
4、测试SLEEP下WDT功耗时通过选项把RTC关闭－－此时分频器是给T0用，时间短，建议用SLEEP测试
5、测试SLEEP下RTC和WDT功耗时通过选项把RTC和WDT同时打开－－此时用WDT唤醒,因为WDT溢出时间短

