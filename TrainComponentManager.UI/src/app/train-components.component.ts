import { Component, OnDestroy, OnInit } from '@angular/core';
import { ComponentService } from './services/component.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForOf, NgIf, NgTemplateOutlet } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { TrainComponent, PaginatedResponse } from './models/component.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-train-components',
  imports: [NgForOf, NgIf, NgTemplateOutlet, FormsModule],
  templateUrl: './train-components.component.html',
  styleUrl: './train-components.component.css'
})
export class TrainComponentsComponent implements OnInit, OnDestroy {
  components: TrainComponent[] = [];
  selectedComponent: TrainComponent | null = null;
  newComponent: TrainComponent | null = null;

  totalCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 10;
  searchTerm: string = '';

  errorMessage: string | null = null;
  private destroy$ = new Subject<void>();

  constructor(
    private componentService: ComponentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }
  
  ngOnInit(): void {
    this.route.queryParams.pipe(takeUntil(this.destroy$)).subscribe(params => {
      this.pageNumber = +params['pageNumber'] || 1;
      this.pageSize = +params['pageSize'] || 10;
      this.searchTerm = params['searchTerm'] || '';
      this.loadComponents();
    })
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  addNewComponent(): void {
    if (!this.selectedComponent && !this.newComponent) {
      this.newComponent = {} as TrainComponent;
    }
  }

  editComponent(component: TrainComponent): void {
    if (!this.selectedComponent && !this.newComponent) {
      this.selectedComponent = { ...component };
    }
  }
  
  cancelCreating(): void {
    this.newComponent = null;
  }
  
  cancelEditing(): void {
    this.selectedComponent = null;
  }
  
  startSearch(): void {
    this.pageNumber = 1;
    this.updatePageQuery();
  }
  
  clearSearch(): void {
    this.searchTerm = '';
    this.pageNumber = 1;
    this.updatePageQuery();
  }
  
  previewPage(): void {
    this.pageNumber--;
    this.updatePageQuery();
  }
  
  nextPage(): void {
    this.pageNumber++;
    this.updatePageQuery()
  }
  
  updatePageQuery(): void {
    const queryParams: any = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      ...(this.searchTerm && { searchTerm: this.searchTerm })   // add search query if it has value
    };
    
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: queryParams,
      queryParamsHandling: 'replace',
    });
  }

  private ensureQuantity(component: TrainComponent): void {
    if (component.quantity === null || component.quantity === undefined) {
      component.quantity = 0;
    }
  }

  private showError(error: HttpErrorResponse, message: string): void {
    this.errorMessage = `${error.statusText}. ${message}`;
    console.error(message, error);
  }
  
  createComponent(): void {
    if (this.newComponent) {
      this.ensureQuantity(this.newComponent);
      this.componentService.createComponent(this.newComponent).pipe(takeUntil(this.destroy$)).subscribe({
        next: () => {
          this.loadComponents();
          this.newComponent = null;
        },
        error: (error: HttpErrorResponse) => this.showError(error, 'An error occurred while creating component')
      })
    }
  }

  loadComponents(): void {
    const serviceCall = this.searchTerm
      ? this.componentService.searchComponents(this.searchTerm, this.pageNumber, this.pageSize)
      : this.componentService.getComponents(this.pageNumber, this.pageSize);
  
    serviceCall.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: PaginatedResponse<TrainComponent>) => {
        this.components = response.items;
        this.totalCount = response.totalCount;
      },
      error: (error: HttpErrorResponse) => this.showError(error, 'An error occurred while loading components')
    });
  }
  
  updateComponent(): void {
    if (this.selectedComponent) {
      let index = this.components.map(c => c.id).indexOf(this.selectedComponent.id);
      this.ensureQuantity(this.selectedComponent);
      this.componentService.updateComponent(this.selectedComponent).pipe(takeUntil(this.destroy$)).subscribe({
        next: () => {
          if (index !== -1 && this.selectedComponent) {
            this.components[index] = this.selectedComponent;
          }
          this.selectedComponent = null;
        },
        error: (error: HttpErrorResponse) => this.showError(error, 'An error occurred while updating components')
      })
    }
  }

  deleteComponent(id: number): void {
    this.componentService.deleteComponent(id).pipe(takeUntil(this.destroy$)).subscribe({
      next: () => this.loadComponents(),
      error: (error: HttpErrorResponse) => this.showError(error, 'An error occurred while deleting components')
    })
  }
}
