/*
 * Created by SharpDevelop.
 * User: Wragon
 * Date: 2012/6/26
 * Time: 10:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CameraControl
{
	/// <summary>
	/// Description of RedGreen.
	/// </summary>
	
	public class Proportion{
		public int low;
		public int hig;
		public double dlow;
		public double dhig;
	}
	
	public class RedGreen
	{
		public RedGreen()
		{
		}
		
		public Proportion Get(int loc, int tot){
			double delta = tot / 4.0;
			int pos = (int)(loc / delta);
			Proportion res = new Proportion();
			res.low = pos;
			res.hig = pos + 1;
			res.dlow = (delta * (pos + 1) - loc) / delta;
			res.dhig = (loc - delta * pos) / delta;
			
			if (res.dlow < 0) res.dlow = 0.0;
			if (res.dlow > 1.0) res.dlow = 1.0;
			if (res.dhig < 0) res.dlow = 0.0;
			if (res.dhig > 1.0) res.dlow = 1.0;
			
			return res;
		}
		
		
		public int[,] Generate(int sx, int sy, int st){
			int[,] res = new int[sx+1, sy+1];
			double dst = st / 4.0;
			for(int y = 0; y <= sy; y++) res[0, y] = 0;
			for(int x = 0; x <= sx; x++) res[x, 0] = 0;
			for(int x = 1; x <= sx; x++) for(int y = 1; y <= sy; y++)
			{
				Proportion tx = Get(x, sx);
				Proportion ty = Get(y, sy);
				double den = 0.0; 
				double mol = 0.0;
				double prm = 0.0;
				if (tx.dlow < ty.dlow) prm = tx.dlow; else prm = ty.dlow;
				den += CommonData.zMo[tx.low, ty.low] * dst * prm;
				mol += prm;
				
				if (tx.dlow < ty.dhig) prm = tx.dlow; else prm = ty.dhig;
				den += CommonData.zMo[tx.low, ty.hig] * dst * prm;
				mol += prm;
				
				if (tx.dhig < ty.dlow) prm = tx.dhig; else prm = ty.dlow;
				den += CommonData.zMo[tx.hig, ty.low] * dst * prm;
				mol += prm;
				
				if (tx.dhig < ty.dhig) prm = tx.dhig; else prm = ty.dhig;
				den += CommonData.zMo[tx.hig, ty.hig] * dst * prm;
				
				res[x, y] = (int)(den / mol);
			}
			return res;
		}
		
	}
}
