import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPage } from './admin-page';

const tabs = [
  { id: 'raffle', label: 'Raffle', icon: 'pi pi-bolt' },
  { id: 'categories', label: 'Categories', icon: 'pi pi-tags' },
  { id: 'donors', label: 'Donors', icon: 'pi pi-users' },
  { id: 'purchases', label: 'Purchases', icon: 'pi pi-shopping-cart' }
];

let activeTab = tabs[0];

