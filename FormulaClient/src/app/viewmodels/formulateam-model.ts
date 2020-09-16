import { IIdprovider } from './iidprovider';

/**
 * Formula team VM
 */
export class FormulaTeam implements IIdprovider  {
  public name: string;
  public yearOfFoundation: number;
  public numberOfChampionshipsWon: number;
  public entryfeePaid: boolean;
  public id: string;


  public constructor(init?: Partial<FormulaTeam>) {
    Object.assign(this, init);
  }
}
