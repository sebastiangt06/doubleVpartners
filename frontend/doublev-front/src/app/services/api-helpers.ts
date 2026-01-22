export function unwrap<T>(res: any): T {
  return (res?.data ?? res?.result ?? res) as T;
}
