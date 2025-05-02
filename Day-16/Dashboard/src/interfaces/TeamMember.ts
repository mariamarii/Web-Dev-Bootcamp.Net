export interface TeamMember {
  id: number;
  name: string;
  position: string;
  department: string;
  email: string;
  phone: string;
  status: 'Full Time' | 'Part Time';
  avatar: string;
  birthday: string;
  address: string;
  teamMates: string;
  hrYears: number;
}