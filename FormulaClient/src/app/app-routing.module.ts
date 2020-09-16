
import { EditTeamComponent } from './components/team/edit-team/edit-team.component';
import { AddTeamComponent } from './components/team/add-team/add-team.component';
import { AuthenticationGuardService } from './guards/authentication-guard.service';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { LogoutComponent } from './components/logout/logout.component';
import { DeleteTeamComponent } from './components/team/delete-team/delete-team.component';


const routes: Routes = [

{
  path: 'me',
  component: WelcomeComponent
},
{
  path: 'login',
  component: LoginComponent
},
{
  path: 'logout',
  component: LogoutComponent, canActivate: [AuthenticationGuardService]
},

{
  path: 'teams/add',
  component: AddTeamComponent,  canActivate: [AuthenticationGuardService]
},

{
  path: 'teams/edit',
  component: EditTeamComponent,  canActivate: [AuthenticationGuardService]
},

{
  path: 'teams/delete',
  component: DeleteTeamComponent,  canActivate: [AuthenticationGuardService]
},
{
  path: '',
  redirectTo: '/me',
  pathMatch: 'full'
},
{
  path: '**',
  redirectTo: '/me',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
