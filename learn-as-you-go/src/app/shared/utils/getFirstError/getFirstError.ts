import { FieldTree, ValidationError } from '@angular/forms/signals';
import { TranslatableError } from './types';

export function getFirstError(fieldTree: FieldTree<unknown>): TranslatableError {
  const firstError = fieldTree().errors()[0] as (ValidationError & Partial<TranslatableError>) | undefined;
  return { message: firstError?.message ?? '', params: firstError?.params };
}