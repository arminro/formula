import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


import { IAppConfig } from './appconfig.model';
import { environment } from './environments/environment';

// based on: https://devblogs.microsoft.com/premier-developer/angular-how-to-editable-config-files/
@Injectable()
export class AppConfig {
    static settings: IAppConfig;
    constructor(private http: HttpClient) {}
    load() {
        const env = environment.production ? 'prod' : 'dev';
        const jsonFile = `assets/config/config.${env}.json`;
        return new Promise<void>((resolve, reject) => {
            this.http.get(jsonFile).toPromise().then((response : IAppConfig) => {
               AppConfig.settings = response as IAppConfig;
               resolve();
            }).catch((response: any) => {
               reject(`Could not load file '${jsonFile}': ${JSON.stringify(response)}`);
            });
        });
    }
}
