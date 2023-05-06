import { Component, Input } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/products';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: IProduct;
}
