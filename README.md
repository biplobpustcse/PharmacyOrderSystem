# PharmacyOrderSystem
#### Migration
```
Add-Migration InitialCreate -Project PharmacyOrderSystem.Infrastructure -StartupProject PharmacyOrderSystem.API
Update-Database -Project PharmacyOrderSystem.Infrastructure -StartupProject PharmacyOrderSystem.API
```

## Frontend Angular integration.
Make sure you have Angular CLI installed:
```
npm install -g @angular/cli
ng new pharmacy-order-app --routing --style=scss
cd pharmacy-order-app
```
Install dependencies for authentication and API calls:
```
npm install @auth0/angular-jwt
npm install bootstrap
```
module app-routing
```
ng generate module app-routing --flat --module=app
```
module features/auth
```
ng generate module features/auth --route auth --module app-routing
```
auth.service.ts
```
ng generate service features/auth/auth
```

