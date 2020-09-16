import { AuthService } from './../../services/auth.service';
import { ComponentBase } from '../component-base';
import { Router } from '@angular/router';
import { TeamsService } from 'src/app/services/teams.service';

export class TeamComponentBase extends ComponentBase {
  constructor(public router: Router, public teamSrv: TeamsService, public authSrv: AuthService) {
    super(router);
  }
}
