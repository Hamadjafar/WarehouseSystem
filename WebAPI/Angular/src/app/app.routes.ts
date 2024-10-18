import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login/login.component';
import { WelcomePageComponent } from './welcome-page/welcome-page/welcome-page.component';
import { AuthGuard } from './service/auth-guard/auth-guard';
import { NgModule } from '@angular/core';
import { WarehouseListComponent } from './Warehouse/warehouse-list/warehouse-list.component';
import { WarehouseViewComponent } from './Warehouse/warehouse-view/warehouse-view/warehouse-view.component';
import { CreateWarehouseComponent } from './Warehouse/create-warehouse/create-warehouse.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { LogsComponent } from './logs/logs/logs.component';
import { UsersComponent } from './users/users/users.component';
import { CreateOrEditUserComponent } from './users/create-or-edit-user/create-or-edit-user/create-or-edit-user.component';


export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'Home', component: WelcomePageComponent, canActivate: [AuthGuard] },
    { path: 'warehouse-list', component: WarehouseListComponent, canActivate: [AuthGuard] },
    { path: 'view-warehouse/:id', component: WarehouseViewComponent, canActivate: [AuthGuard] },
    { path: 'create-warehouse', component: CreateWarehouseComponent, canActivate: [AuthGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },  
    { path: 'logs', component: LogsComponent, canActivate: [AuthGuard] },  
    { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    { path: 'create-or-edit-user', component: CreateOrEditUserComponent, canActivate: [AuthGuard] },
    { path: 'create-or-edit-user/:id', component: CreateOrEditUserComponent, canActivate: [AuthGuard] }
    
    
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }
