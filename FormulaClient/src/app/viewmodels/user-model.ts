import { IIdprovider } from './iidprovider';

/**
 * VM for holding data regarding user data.
 */
export class User implements IIdprovider  {
  public username: string;
  public description: string;
  public name: string;
  public id: string;

  public token: string;

  // this serves to provide a c#-like initialization
  public constructor(init?: Partial<User>) {
    Object.assign(this, init);
  }
}
