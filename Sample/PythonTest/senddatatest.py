import socketio
import asyncio
from socketio import Client
import time

# step 1 open cmd , enter : pip install "python-socketio[asyncio_client]"

# step 2 open cmd , enter : pip install asyncio

async def send_data_to_center():
    sio = socketio.AsyncClient()
    event = 'message'

    @sio.event()
    async def my_response(data):
        # handle the message
        # sio.emit('my_event', {"cmd": "joinRoom", "roomId": 8888})
        print(data)

    @sio.event
    async def connect():
        print("I'm connected!")

    @sio.event
    def connect_error():
        print("The connection failed!")

    @sio.event
    def disconnect():
        print("I'm disconnected!")

    url = 'http://127.0.0.1:8088'
    await sio.connect(url,transports=["websocket"])
    print('my sid is', sio.sid)

    # 必需进行注册和加入room操作,room等于发布教学活动的activityid
    await sio.emit(event, {"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Data from python ...","Type":"Test Type","Name":"Test name","Id":1234})
    await sio.emit(event, {"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Data from python ...","Type":"Test Type","Name":"Test name","Id":1234})
    await sio.emit(event, {"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Data from python ...","Type":"Test Type","Name":"Test name","Id":1234})
    await sio.emit(event, {"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Data from python ...","Type":"Test Type","Name":"Test name","Id":1234})




if __name__ == '__main__':
    asyncio.run(send_data_to_center())
    # while(loop):
    #     time.sleep(0.1)
