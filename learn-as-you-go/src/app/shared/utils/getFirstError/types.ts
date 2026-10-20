export interface TranslatableError {
  readonly message: string,
  readonly params?: Record<string, unknown>
}