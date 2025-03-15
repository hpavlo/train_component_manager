import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainComponentsComponent } from './train-components.component';

describe('TrainComponentsComponent', () => {
  let component: TrainComponentsComponent;
  let fixture: ComponentFixture<TrainComponentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrainComponentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
