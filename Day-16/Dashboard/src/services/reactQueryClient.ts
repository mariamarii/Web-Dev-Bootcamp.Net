import { useQuery } from '@tanstack/react-query';
import { TeamMember } from '../interfaces/TeamMember';
import { fetchTeamMembers } from '../api/api';

export const useTeamMembers = (page: number, rowsPerPage: number, searchTerm: string) => {
  const { data: teamMembers = [], isLoading, isError, error } = useQuery<TeamMember[], Error>({
    queryKey: ['teamMembers', page, rowsPerPage, searchTerm],
    queryFn: () => fetchTeamMembers(page, rowsPerPage),
    staleTime: 5 * 60 * 1000, // 5 minutes
    retry: 3,
  });

  // Mock totalCount since the API doesn't provide it
  const totalCount = 100; // Assume 100 total members for pagination

  return {
    teamMembers,
    totalCount,
    isLoading,
    isError,
    error,
  };
};