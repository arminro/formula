import { Observable} from 'rxjs';

/**
 * Interface for common CRUD operations.
 * @template TEntity a generic type parameter for a VM entity
 */
export interface IDataService<TEntity> {
  getElements(): Observable<TEntity[]>;

  getElement(id: number): Observable<TEntity>;

  createElement(entity: TEntity);

  updateElement(entity: TEntity);

  deleteElement(entity: TEntity);
}
