package trees;

import containers.Container;

/**
 * Base interface for binary trees.
 * Author:   Sergei Golitsinski.
 * Created:  May 24, 2004.
 * Modified: May 24, 2004.
 */
public interface BinaryTree extends Container
{
	public int levels();
	public BinaryTreeIterator makePreOrderIterator();
	public BinaryTreeIterator makeInOrderIterator();
	public BinaryTreeIterator makePostOrderIterator();
	public BinaryTreeIterator makeLevelOrderIterator();
}
