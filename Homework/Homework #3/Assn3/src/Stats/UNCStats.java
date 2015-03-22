package Stats;

import java.util.*;
import java.text.*;

public class UNCStats
{
	public static void main(String[] args)
	{	
		Statistics unc = new Statistics();
		DecimalFormat df = new DecimalFormat("0.00");
		unc.setFieldGoalsMade(841);
		unc.setFieldGoalAttempts(1753);
		unc.setFreeThrowsMade(524);
		unc.setFreeThrowAttempts(692);
		unc.set3PointersMade(145);
		unc.set3PointAttempts(398);
		
		int points = unc.getTotalPoints();
		//int miss = unc.getMisses();
		double field = unc.getFieldGoalPercent();
		double free = unc.getFreeThrowPercent();
		double three = unc.get3PointPercent();
		System.out.print("UNC has scored " + points + " points\n" +
				"UNC has a field-goal percentage of " + df.format(field) + "%\n" +
				"UNC has a free-throw percentage of " + df.format(free) + "%\n" +
	 			"UNC has a 3-point field-goal percentage of " + df.format(three) + "%\n");	
	}
		
}
