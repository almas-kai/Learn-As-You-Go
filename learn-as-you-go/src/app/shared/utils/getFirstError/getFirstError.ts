import { FieldTree } from '@angular/forms/signals';

export function getFirstError(fieldTree: FieldTree<unknown>): string {
  const errors = fieldTree().errors();
  return errors[0]?.message ?? '';
} 