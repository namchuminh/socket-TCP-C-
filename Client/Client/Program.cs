using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Bước 1: Xác đinh IpEndpoint của server TCP
                IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                //Bước 2: Tạo socket TCP (client)
                Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                //Bước 3: Tạo kết nối tới server TCP
                clientSocket.Connect(s_iep);
                Console.WriteLine("Ket noi thanh cong toi Server!");
                //Bước 4: Trao đổi (nhận/gửi) tin giữa clinet và server

                do
                {
                    //Gửi tin
                    string messageSend = Console.ReadLine(); //Nhập chuỗi gửi lên server
                    byte[] dataSend = ASCIIEncoding.ASCII.GetBytes(messageSend); //chuyển chuỗi sang kiểu byte
                    clientSocket.Send(dataSend); //Gửi dữ liệu lên server

                    //Nhận tin từ server gửi về
                    byte[] dataReceive = new byte[1024]; //Khai báo nhận tin vào biến dataRecevie
                    clientSocket.Receive(dataReceive); //nhận dữ liệu trả về từ server và gán vào biến dataReceive
                    string messageReceive = ASCIIEncoding.ASCII.GetString(dataReceive);//Chuyển byte thành string
                    Console.WriteLine("<server>: " + messageReceive);

                    if (messageSend.ToLower() == "thoat" || messageReceive.ToLower() == "thoat")
                    {
                        break;
                    }

                } while (true);
                

                
                //Bước 5: Đóng kết nối
                clientSocket.Close();
            }catch(Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
