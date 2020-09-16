// based on: https://devblogs.microsoft.com/premier-developer/angular-how-to-editable-config-files/
export interface IAppConfig {
  name: string;
  apiServer: {
    baseUrl: string,
    mainSegment: string,
    loginSegment: string,
    logoutSegment: string
  }
}
