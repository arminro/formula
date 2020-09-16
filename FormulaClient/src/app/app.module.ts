import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AlertModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { DateValueAccessorModule } from 'angular-date-value-accessor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { AddTeamComponent } from './components/team/add-team/add-team.component';
import { EditTeamComponent } from './components/team/edit-team/edit-team.component';
import { DeleteTeamComponent } from './components/team/delete-team/delete-team.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppHttpInterceptor } from './interceptors/http-interceptor';
import { FormsModule } from '@angular/forms';
import { AppConfig } from 'src/appconfig';

export function initApp(appConfig: AppConfig) {
  console.log(appConfig);
  return () => appConfig.load();
}


@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    LoginComponent,
    LogoutComponent,
    AddTeamComponent,
    EditTeamComponent,
    DeleteTeamComponent,
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    DateValueAccessorModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor, multi: true
    },
    AppConfig,
    { provide: APP_INITIALIZER,
      useFactory: initApp,
      deps: [AppConfig], multi: true }

  ],
  bootstrap: [AppComponent],
  entryComponents: [LogoutComponent],

})
export class AppModule { }
