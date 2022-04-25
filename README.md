# CommonLogCollectSystem
A set of Client/Server Communication Tools to send data from client to server .Then data will automatic classify on server side tab by tab .
It can be used to collect log data when doing Programme Trading . 
It includes Server Winform application, and client application.
It can communication via socket.io or wcf.

Server side was built by .net c#.

Client side can be coded by Java ,Python, Node.js.
CommonLogCollectSystem V2.1

通用日志收集系统

适用场景 程序化交易中监控收集策略运行日志
使用说明

 
 
本系统经生产环境测试，连续使用1个月以上，24小时不间断接收来自多个终端的数据。接收到数据后，根据输入时输入的TableName="Test,TestTab1";这个TableName格式不能修改,逗号前面代表上图的大TAB，逗号后面是子TAB名称。会根据内容自动创建TAB及表格窗口。
可以跨网络传输。现在支持JAVA，PYTHON,NODE.JS语言编写的客户端与之通讯。如果不是.NET 客户端，则需要在客户端安装NODE.JS。具体见安装目录下Sample文件夹
 

1．	服务端配置如下 
如下图，服务端WCF 端口设置2，现在是9090，可根据实际情况填入，如修改了这个端口，.NET 客户端如果使用WCF连接，也需要修改。
 

2．	服务端SOCKET.IO 通信端口设置如上图1位置，现在是8088，如果修改了，NODE.JS 客户端如下位置也需要修改。默认连接地址是http://127.0.0.1:8088  ,JAVA,PYTHON也是用相同的地址端口连接
 

如需要帮助，点击服务端菜单下其它-》帮助按钮。


Author
foibble@163.com


