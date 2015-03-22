package Stats;

class Statistics {
	
	private int freeThrowsMade;
	private int freeThrowsAttempted;	
	
	private int fieldGoalsMade;
	private int fieldGoalsAttempted;
	
	private int threePointersMade;
	private int threePointersAttempted;
	
	/*------------------------------------------------//
	//           Set the Input Variables              //
	//------------------------------------------------*/	
	
	protected void setFreeThrowsMade(int numbFreeThrows){
		freeThrowsMade = numbFreeThrows;
	}
	
	protected void setFreeThrowAttempts(int numbAttempts){
		freeThrowsAttempted = numbAttempts;
	}
	
	protected void setFieldGoalsMade(int numbFieldGoals){
		fieldGoalsMade = numbFieldGoals;
	}
	
	protected void setFieldGoalAttempts(int numbAttempts){
		fieldGoalsAttempted = numbAttempts;
	}		

	protected void set3PointersMade(int numb3Pointers){
		threePointersMade = numb3Pointers;
	}
	
	protected void set3PointAttempts(int numbAttempts){
		threePointersAttempted = numbAttempts;
	}		
	
	/*------------------------------------------------//
	//         Return Percentage Information          //
	//------------------------------------------------*/

	protected double getFreeThrowPercent(){
		return 100.0 * freeThrowsMade / freeThrowsAttempted;
	}	
	
	protected double getFieldGoalPercent(){
		return 100.0 * fieldGoalsMade / fieldGoalsAttempted;
	}
	
	protected double get3PointPercent(){
		return 100.0 * threePointersMade / threePointersAttempted;
	}

	/*------------------------------------------------//
	//          Calculate the Total Points           //
	//------------------------------------------------*/	
	
	protected int getTotalPoints(){
		return freeThrowsMade + 2*fieldGoalsMade + 3*threePointersMade;
	}

}
