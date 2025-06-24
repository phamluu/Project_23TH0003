Cập nhật web.config

<add name="DefaultConnection" connectionString="Data Source=ADMIN\SQLEXPRESS01;Initial Catalog=Project_23TH0003;Integrated Security=True" providerName="System.Data.SqlClient" />

<add name="Project_23TH0003Entities" connectionString="metadata=res://*/Models.Model_23TH0003.csdl|res://*/Models.Model_23TH0003.ssdl|res://*/Models.Model_23TH0003.msl;
	 provider=System.Data.SqlClient;provider connection string=&quot;data source=ADMIN\SQLEXPRESS01;initial catalog=Project_23TH0003;integrated security=True;encrypt=False;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

<h3>Lệnh tạo migration</h3>
<p>dotnet ef migrations add CreateQLHocPhanSchema --context QuanLyHocPhanDbContext --output-dir Data/Migrations/QLHocPhan</p>
<h3>Cập nhật lại database sau khi tạo migration</h3>
<p>dotnet ef database update --context QuanLyHocPhanDbContext
</p>
