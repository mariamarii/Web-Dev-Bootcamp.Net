public class Solution {
    private ListNode current = null;

    public TreeNode SortedListToBST(ListNode head)
    {
        int size = GetLength(head);
        current = head;
        return BuildBST(0, size - 1);
    }

    private int GetLength(ListNode head)
    {
        int count = 0;
        while (head != null)
        {
            count++;
            head = head.next;
        }
        return count;
    }

    private TreeNode BuildBST(int left, int right)
    {
        if (left > right)
            return null;

        int mid = (left + right + 1) / 2;

        TreeNode leftNode = BuildBST(left, mid - 1);

        TreeNode root = new TreeNode(current.val);
        current = current.next;
        root.left = leftNode;
        TreeNode rightNode = BuildBST(mid + 1, right);
        root.right = rightNode;

        return root;
    }
}