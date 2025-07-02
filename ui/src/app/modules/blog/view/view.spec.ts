import { ComponentFixture, TestBed } from '@angular/core/testing';

import { View } from './view';

describe('View', () => {
  let component: View;
  let fixture: ComponentFixture<View>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [View]
    })
    .compileComponents();

    fixture = TestBed.createComponent(View);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
