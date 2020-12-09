import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../Interfaces/IProduct';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { Observable, BehaviorSubject } from 'rxjs';
import { IProductColour } from '../Interfaces/IProductColour';
import { IProductSize } from '../Interfaces/IProductSize';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  cartProduct: IProduct[] = [];
  private messageProduct:  BehaviorSubject<IProduct[]> = new BehaviorSubject<IProduct[]>(this.cartProduct);
  getMessage = this.messageProduct.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string)
  {
  }

  getAll() {
    return this.http.get<IProduct[]>(this.baseUrl + 'Product/GetAll');
  }

  createAddress() {
    var r = this.http.post<any>(this.baseUrl + 'Checkout/CreateAddress/', null);
    return r;
  }

  getCountries() {
    var r = this.http.get<any>(this.baseUrl + 'Checkout/GetCountries');
    return r;
  }

  getAllSizes() {
    return this.http.get<IProductSize[]>(this.baseUrl + 'Product/GetAllSizes');
  }

  getAllColours() {
    return this.http.get<IProductColour[]>(this.baseUrl + 'Product/GetAllColours');
  }

  setMessage(cartItems: IProduct[] ) {
    this.messageProduct.next(cartItems);
  }
}
