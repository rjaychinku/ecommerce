import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../Interfaces/IProduct';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { Observable, BehaviorSubject } from 'rxjs';
import { IProductColour } from '../Interfaces/IProductColour';
import { IProductSize } from '../Interfaces/IProductSize';
import { filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private messageProduct = new BehaviorSubject<IProduct[]>([]);
  public getMessage = this.messageProduct.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  getAll(): Promise<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + 'Product/GetAll').toPromise();
  }
 
  createAddress() {
    var r = this.http.post<any>(this.baseUrl + 'Checkout/CreateAddress/', null);
    return r;
  }

  getCountries() {
    return this.http.get<any>(this.baseUrl + 'Checkout/GetCountries');
  }

  getAllSizes(): Promise<IProductSize[]> {
    return this.http.get<IProductSize[]>(this.baseUrl + 'Product/GetAllSizes').toPromise();
  }

  getAllColours(): Promise<IProductColour[]> {
    return this.http.get<IProductColour[]>(this.baseUrl + 'Product/GetAllColours').toPromise();
  }

  setMessage(cartItems: IProduct[]) {
    this.messageProduct.next(cartItems);
  }
}
