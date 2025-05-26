import { Injectable } from '@angular/core';
import { v4 as uuidv4 } from 'uuid';
@Injectable({
  providedIn: 'root'
})
export class GuidService {

// Generate a new GUID
  generate(): string {
    return uuidv4();
  }

  // Generate and store a GUID in localStorage under a given key (if not already present)
  getOrCreate(key: string): string {
    let guid = localStorage.getItem(key);
    if (!guid) {
      guid = this.generate();
      localStorage.setItem(key, guid);
    }
    return guid;
  }

  // Retrieve a stored GUID from localStorage (null if not found)
  get(key: string): string | null {
    return localStorage.getItem(key);
  }

  // Overwrite the stored GUID under a specific key
  set(key: string, guid: string): void {
    localStorage.setItem(key, guid);
  }

  // Remove a stored GUID from localStorage
  remove(key: string): void {
    localStorage.removeItem(key);
  }

  // Generate a new GUID and replace the one in localStorage under a given key
  reset(key: string): string {
    const newGuid = this.generate();
    localStorage.setItem(key, newGuid);
    return newGuid;
  }
}
