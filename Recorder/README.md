# Recorder

## 1. Giới thiệu về dự án

Recorder là ứng dụng desktop Windows Forms dùng để ghi âm và làm việc với hệ thống xử lý âm thanh của dự án Bóc Băng. Ứng dụng hỗ trợ hai luồng chính:

- Ghi âm online: thu âm từ microphone, gửi dữ liệu âm thanh theo thời gian thực lên máy chủ qua WebSocket và nhận kết quả trả về.
- Ghi âm offline: thu âm cục bộ, lưu file âm thanh và đồng bộ thông tin phiên ghi với backend qua HTTP API.

Luồng chạy chính của ứng dụng:

1. Mở ứng dụng và hiển thị màn hình đăng nhập.
2. Gọi API đăng nhập để lấy token xác thực.
3. Nạp cấu hình thiết bị ghi âm, URL API, URL WebSocket và thư mục lưu dữ liệu.
4. Thu âm bằng NAudio.
5. Gửi dữ liệu âm thanh hoặc metadata phiên ghi lên backend.
6. Lưu log và file âm thanh ra thư mục output.

Thông tin kỹ thuật chính:

- Loại ứng dụng: Windows Forms.
- Framework: .NET Framework 4.5.
- Output: WinExe.
- File dự án: Recorder.csproj.

## 2. Dependencies và cách cài đặt

### 2.1. Môi trường phát triển

- Hệ điều hành: Windows.
- Microsoft Visual Studio hiện có trên máy: Visual Studio Community 2019 phiên bản 16.11.44.
- Định dạng project: csproj kiểu cũ, khai báo ToolsVersion 15.0.
- Workload nên cài trong Visual Studio: .NET desktop development.

Khuyến nghị:

- Dùng Visual Studio 2019 để đồng nhất với môi trường đã kiểm chứng build thành công.
- Có thể dùng Visual Studio 2022 nếu đã cài đầy đủ .NET desktop development và hỗ trợ .NET Framework targeting pack tương ứng.

### 2.2. Framework và công cụ bắt buộc

- .NET Framework 4.5 Targeting Pack.
- MSBuild đi kèm Visual Studio 2019 hoặc Build Tools tương đương.
- NuGet Package Restore.

### 2.3. Thư viện NuGet đang sử dụng

Project dùng packages.config và tham chiếu package từ thư mục ../packages.

| Package | Version | Mục đích |
| --- | --- | --- |
| CredentialManagement | 1.0.2 | Lưu username/password vào Windows Credential Manager |
| log4net | 2.0.8 | Ghi log hệ thống |
| NAudio | 1.10.0 | Thu âm từ microphone và xử lý audio |
| NAudio.Lame | 1.0.9 | Hỗ trợ mã hóa/làm việc với MP3 |
| Newtonsoft.Json | 12.0.3 | Serialize/deserialize JSON |
| SuperSocket.ClientEngine.Core | 0.10.0 | Thành phần hỗ trợ kết nối client |
| WebSocketSharp-netstandard | 1.0.1 | Giao tiếp WebSocket |

Ngoài các package trên, ứng dụng còn dùng thư viện chuẩn của .NET như System.Configuration, System.Windows.Forms, System.Drawing, System.Net.Http, System.Web.Extensions.

### 2.4. Cách cài đặt môi trường

#### Cách 1: Cài bằng Visual Studio Installer

1. Cài Microsoft Visual Studio 2019 Community 16.11.x.
2. Trong Visual Studio Installer, chọn workload .NET desktop development.
3. Bảo đảm đã cài .NET Framework 4.5 targeting pack.
4. Mở file Recorder.csproj bằng Visual Studio.
5. Nếu Visual Studio hỏi restore package, chọn Restore.

#### Cách 2: Khôi phục package bằng NuGet thủ công

Nếu máy mới chưa có thư mục packages ở cấp cha của project, chạy lệnh sau tại thư mục AudioRecorder:

```powershell
nuget restore .\Recorder\Recorder.csproj
```

Lưu ý:

- Project hiện đang tham chiếu package theo đường dẫn tương đối ../packages.
- Nghĩa là thư mục packages phải nằm cùng cấp với thư mục Recorder.

### 2.5. Dependencies ngoài source code

Để chạy đúng nghiệp vụ, ứng dụng còn phụ thuộc vào các cấu hình và dịch vụ bên ngoài:

- API backend cấu hình qua key api.
- WebSocket server cấu hình qua key ws.
- Microphone đầu vào trên máy người dùng.
- Quyền ghi file vào thư mục lưu audio.

Cấu hình mặc định nằm trong App.config và phần user settings của ứng dụng, ví dụ:

- ws: địa chỉ WebSocket server.
- api: địa chỉ HTTP API.
- mic: microphone mặc định.
- audiolength: thời lượng audio mặc định.
- datadir: thư mục lưu dữ liệu.

## 3. Kiến trúc của source code

Mã nguồn được tổ chức theo hướng tách giao diện, điều khiển nghiệp vụ, dữ liệu và cấu hình dùng chung.

### 3.1. Cấu trúc thư mục chính

- AppForm/: các form giao diện người dùng.
- Controller/: lớp điều khiển ghi âm, stream audio và gọi request nghiệp vụ.
- Common/: cấu hình dùng chung, tiện ích và logging.
- Data/: model dữ liệu nội bộ.
- DataMessage/: model message trao đổi với backend/WebSocket.
- Login/: lưu trữ thông tin đăng nhập.
- CustomControl/: custom control cho giao diện.
- Properties/: tài nguyên, settings và assembly metadata.
- Resources/: icon và ảnh dùng cho UI.

### 3.2. Vai trò của các thành phần chính

#### Entry point

- Program.cs:
	- Cấu hình TLS.
	- Mở LoginForm để xác thực người dùng.
	- Sau khi đăng nhập thành công thì chạy AudioRecordingForms.

#### Tầng giao diện

- AppForm/LoginForm.cs:
	- Hiển thị màn hình đăng nhập.
	- Gọi RequestLogin để lấy token.
	- Lưu username/password nếu người dùng chọn ghi nhớ.

- AppForm/RecordingForm.cs:
	- Form nghiệp vụ chính.
	- Điều khiển trạng thái ghi âm online/offline.
	- Nhận dữ liệu microphone, gửi audio lên server, cập nhật giao diện và lưu file audio nếu cần.

- AppForm/SettingForm.cs:
	- Cấu hình URL server, microphone mặc định, thời lượng audio và thư mục lưu dữ liệu.

- AppForm/AudioTesting.cs, FunctionSelection.cs, AppGates.cs:
	- Các màn hình hỗ trợ test/chọn chức năng/cổng vào ứng dụng.

#### Tầng nghiệp vụ và tích hợp

- Controller/Recorder.cs:
	- Bao gói NAudio.WaveInEvent.
	- Khởi tạo thiết bị ghi âm, đăng ký callback, start/stop record.

- Controller/AudioStreamer.cs:
	- Quản lý kết nối WebSocket.
	- Gửi audio/message lên server.
	- Nhận text result từ backend và phát sự kiện về giao diện.
	- Có logic reconnect khi kết nối bị lỗi.

- Controller/AudioWriter.cs:
	- Ghi dữ liệu audio xuống file.

- Controller/Request.cs:
	- Gọi HTTP API cho đăng nhập, tạo/cập nhật phiên ghi, đồng bộ metadata audio.

#### Tầng cấu hình và tiện ích

- Common/AppsSettings.cs:
	- Singleton đọc/ghi cấu hình ứng dụng.
	- Lưu các giá trị ws, api, mic, audiolength, datadir.

- Common/AppLogger.cs:
	- Khởi tạo log4net và ghi log hệ thống.

- Common/Utils.cs:
	- Hàm tiện ích dùng chung như xử lý URL, chuỗi và helper khác.

#### Tầng dữ liệu

- Data/:
	- Chứa các model dữ liệu nội bộ như AudioFileProperty, AudioTestResult.

- DataMessage/:
	- Chứa các message trao đổi với backend như AudioBinaryMessage, TextResultMessage, BackEndResponseMessage, LoginSuccessfullyMessage.

#### Lưu thông tin đăng nhập

- Login/PasswordRepository.cs:
	- Dùng CredentialManagement để lưu username/password vào Windows Credential Manager.

### 3.3. Luồng xử lý tổng quát

1. Người dùng đăng nhập tại LoginForm.
2. Request gửi thông tin đăng nhập lên backend và nhận token.
3. AppsSettings nạp cấu hình làm việc.
4. AudioRecordingForms khởi tạo microphone, timer, worker và UI.
5. Recorder lấy mẫu âm thanh từ thiết bị input.
6. AudioStreamer gửi dữ liệu lên WebSocket server.
7. Backend trả kết quả text hoặc trạng thái phiên làm việc.
8. AudioWriter và log4net ghi file audio/log cục bộ khi cần.

## 4. Cách build ra file .exe

Phần này đã được kiểm chứng trên máy Windows với Visual Studio Community 2019 16.11.44. Build Release thành công và sinh file:

```text
Recorder\bin\Release\Recorder.exe
```

### 4.1. Build bằng Visual Studio

1. Mở Recorder.csproj bằng Visual Studio.
2. Chờ Visual Studio restore package nếu được yêu cầu.
3. Chọn cấu hình Release.
4. Chọn nền tảng Any CPU.
5. Chọn Build > Build Solution.

File output sau khi build:

```text
Recorder\bin\Release\Recorder.exe
```

Các file đi kèm cần giữ cùng thư mục output:

- Recorder.exe.config
- App.config
- log4net.config
- Các DLL dependency như NAudio.dll, log4net.dll, Newtonsoft.Json.dll, websocket-sharp.dll, CredentialManagement.dll
- libmp3lame.32.dll và libmp3lame.64.dll

### 4.2. Build bằng command line

Mở PowerShell hoặc Developer Command Prompt tại thư mục Recorder, sau đó chạy:

```powershell
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" .\Recorder.csproj /t:Restore,Build /p:Configuration=Release /p:Platform=AnyCPU /nologo
```

Nếu build thành công, file .exe sẽ nằm tại:

```text
bin\Release\Recorder.exe
```

### 4.3. Kiểm tra sau build

Sau khi build, nên kiểm tra các điểm sau:

- Có file Recorder.exe trong bin\Release.
- Có file cấu hình Recorder.exe.config và log4net.config.
- Có đầy đủ DLL phụ thuộc được copy sang thư mục output.
- Máy chạy có microphone hoạt động bình thường.
- API URL và WebSocket URL trong cấu hình trỏ đúng môi trường cần dùng.

### 4.4. Một số lỗi thường gặp

- Thiếu package trong thư mục ../packages:
	- Khắc phục bằng NuGet restore.

- Thiếu .NET Framework 4.5 targeting pack:
	- Cài thêm trong Visual Studio Installer.

- Không kết nối được API/WebSocket khi chạy:
	- Kiểm tra lại giá trị api và ws trong cấu hình.

- Không ghi âm được:
	- Kiểm tra microphone mặc định, quyền truy cập thiết bị và chỉ số mic đang được chọn.
