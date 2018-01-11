package lsp.violation;

public class Square extends Rectangle{

	public Square(int width) {
		super(width,width);
	}
	
	@Override
	public void setWidth(int paramwidth) {
		this.width=this.height = paramwidth;
	}

	@Override
	public void setHeight(int paramheight) {
		this.height = this.width = paramheight;
	}


	
}
