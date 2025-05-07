# PharmacyOrderSystem
#### Migration
```
Add-Migration InitialCreate -Project PharmacyOrderSystem.Infrastructure -StartupProject PharmacyOrderSystem.API
Update-Database -Project PharmacyOrderSystem.Infrastructure -StartupProject PharmacyOrderSystem.API
```
