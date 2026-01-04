export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  name: string;
  email: string;
  password: string;
  phone?: string;
  city?: string;
  address?: string;
}

export interface UserResponseDto {
  id: number;
  name: string;
  email: string;
  phone: string;
  city: string;
  address: string;
  role: 'Admin' | 'Donor' | 'User';
}

export interface LoginResponseDto {
  token: string;
  tokenType: string;
  expiresIn: number;
  user: UserResponseDto;
}
