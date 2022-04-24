package com.dasnnj.practice.share.socket;
import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import io.socket.client.IO;
import io.socket.client.Socket;
import org.junit.Test;

import java.util.Arrays;

/**
 * Description
 TODO : socket.io client端

 pom.xml content

 <dependency>
 <groupId>io.socket</groupId>
 <artifactId>socket.io-client</artifactId>
 <version>1.0.0</version>
 </dependency>
 <dependency>
 <groupId>com.corundumstudio.socketio</groupId>
 <artifactId>netty-socketio</artifactId>
 <version>1.7.16</version>
 </dependency>
 <dependency>
 <groupId>org.slf4j</groupId>
 <artifactId>slf4j-nop</artifactId>
 <version>1.7.2</version>
 </dependency>


 */
class StrategyData {

    public String TableName;

    public String Time;

    public String Content;

    public String Type;

    public String Name;

    public int Id;
    //public string HostName { get; set; }

}
public class SocketIoTest {

    @Test
    public static void main(String[] args) {
        String url = "http://127.0.0.1:8088";
        try {
            IO.Options options = new IO.Options();
            options.transports = new String[]{"websocket"};
            //失败重试次数
            options.reconnectionAttempts = 10;
            //失败重连的时间间隔
            options.reconnectionDelay = 1000;
            //连接超时时间(ms)
            options.timeout = 500;
            final Socket socket = IO.socket(url, options);
            //监听自定义msg事件
            socket.on("msg", objects -> System.out.println("client: 收到msg->" + Arrays.toString(objects)));
            //监听自定义订阅事件
            socket.on("sub", objects -> System.out.println("client: " + "订阅成功，收到反馈->" + Arrays.toString(objects)));
            StrategyData data=new StrategyData();
            data.Content="Data from java";
            data.TableName="Test,TestTab1";
            data.Type="Test type";
            data.Name="Test Name";
            socket.on(Socket.EVENT_CONNECT, objects -> {
                socket.emit("message",JSON.toJSONString(data) );
                System.out.println("client: " + "连接成功");
            });
            socket.on(Socket.EVENT_CONNECTING, objects -> System.out.println("client: " + "连接中"));
            socket.on(Socket.EVENT_CONNECT_TIMEOUT, objects -> System.out.println("client: " + "连接超时"));
            socket.on(Socket.EVENT_CONNECT_ERROR, objects -> System.out.println("client: " + "连接失败"));
            socket.connect();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}
