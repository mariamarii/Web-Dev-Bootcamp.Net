import axios from 'axios';
import { TeamMember } from '../interfaces/TeamMember'; // Used for return type

const positions = [
  'Graphics Designer',
  'Joomla Developer',
  'Human Resource',
  'PHP Developer',
  'UI/UX Designer',
  'UI/UX Architect',
  'Python Developer',
  'Freshers',
];
const departments = [
  'Sales Team',
  'Finances',
  'Management',
  'Engineering',
  'Human Resources',
  'Customer Success',
  'Marketing',
  'Product',
];

export const fetchTeamMembers = async (page: number, results: number): Promise<TeamMember[]> => {
  try {
    const response = await axios.get(`https://randomuser.me/api?page=${page}&results=${results}`);
    const data = response.data;

    return data.results.map((user: any, index: number) => {
      const allNames = data.results.map((u: any) => `${u.name.first} ${u.name.last}`);
      const otherNames = allNames.filter(
        (name: string) => name !== `${user.name.first} ${user.name.last}`
      );
      const randomTeamMates = otherNames
        .sort(() => 0.5 - Math.random())
        .slice(0, 3)
        .join(', ');

      return {
        id: (page - 1) * results + index + 1,
        name: `${user.name.first} ${user.name.last}`,
        position: positions[Math.floor(Math.random() * positions.length)],
        department: departments[Math.floor(Math.random() * departments.length)],
        email: user.email,
        phone: user.phone,
        status: Math.random() > 0.5 ? 'Full Time' : 'Part Time',
        avatar: user.picture.thumbnail,
        birthday: new Date(user.dob.date).toLocaleDateString(),
        address: `${user.location.street.number} ${user.location.street.name}, ${user.location.city}, ${user.location.state} ${user.location.postcode}`,
        teamMates: randomTeamMates || 'N/A',
        hrYears: Math.floor(Math.random() * 10) + 1,
      };
    });
  } catch (error) {
    console.error('Error fetching team members:', error);
    throw error;
  }
};