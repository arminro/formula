import { AppConfig } from './../../appconfig';
import { FormulaTeam } from '../viewmodels/formulateam-model';
import { Injectable } from '@angular/core';
import { ServiceBase } from './servicebase';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

// based on: https://www.djamware.com/post/5bca67d780aca7466989441f/angular-7-tutorial-building-crud-web-application

/**Service for data operations concerning FormulaTeam entities
 *
 * Injectable
 */
@Injectable({
  providedIn: 'root'
})
export class TeamsService extends ServiceBase<FormulaTeam> {
  constructor(protected readonly http: HttpClient, protected readonly authSrv: AuthService) {
    super(http, authSrv);
  }
  urlSegment = AppConfig.settings.apiServer.mainSegment;

}
