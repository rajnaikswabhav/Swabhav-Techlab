package lsp.violation.test;

import lsp.violation.Rectangle;
import lsp.violation.Square;

public class TestLSPViolation {

	public static void main(String[] args) {

		Rectangle rectangle = new Rectangle(5,2);
		shouldNot_ChangeHeight_IfWidthWasChange(rectangle);
		shouldNotChangeWidthIfHeightWasChange(rectangle);
		
		Square square = new Square(5);
		shouldNot_ChangeHeight_IfWidthWasChange(square);
		shouldNotChangeWidthIfHeightWasChange(square);
	}
	
	public static void shouldNot_ChangeHeight_IfWidthWasChange(Rectangle rectangle)
	{
		int heightBeforeChange = rectangle.getHeight();
		rectangle.setWidth(50);
		int heightAfterChange = rectangle.getHeight();
		
		System.out.println("Height Before Change: "+heightBeforeChange);
		System.out.println("Height After Change: "+heightAfterChange);
	}
	
	public static void shouldNotChangeWidthIfHeightWasChange(Rectangle rectangle)
	{
		int widthBeforeChange = rectangle.getWidth();
		rectangle.setHeight(50);
		int widthAfterChange = rectangle.getWidth();
		
		System.out.println("Width Before Change: "+widthBeforeChange);
		System.out.println("Width After Change: "+widthAfterChange);
	}

}
