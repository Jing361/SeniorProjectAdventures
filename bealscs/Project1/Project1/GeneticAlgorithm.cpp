#include "GeneticAlgorithm.h"

using namespace DarwinCharles;

int GeneticAlgorithm::run ()
{

  initialize ( "simple_ga_input.txt" );

  evaluate ( );

  keep_the_best () ;
  
  std::sort(population.begin(), population.end(), compareFitness);

  //for (int generation =  0; generation < MAXGENS; generation++ )
  //{
    
    //crossover ( );
    //mutate ( );
    //report ( generation );
    //evaluate ( );
	//selector ( );
    //elitist ( );
  //}

  cout << "\n";
  cout << "\n";
  cout << "Simulation completed.\n";

  cout << "\n";
  cout << rangedPercent << "\n" << meleePercent << "\n" << blockPercent << "\n" << dashPercent << "\n" << enemyDPSwing 
	  << "\n" << enemyDPShot << "\n";

  vector<int>::iterator geneIterator;

   cout << "var("  ") = " << "\n";

	for(int j = 0; j <= POPSIZE; ++j)
	{
		
		for(geneIterator = population[j].getGene().begin(); geneIterator != population[j].getGene().end(); ++geneIterator)
		{
			cout << *geneIterator << "  ";

		}

		cout << "\n";
		
		cout << population[j].getFitness() << "\n";

    }

	for (geneIterator = population[POPSIZE].getGene().begin(); geneIterator != population[POPSIZE].getGene().end(); ++geneIterator)
	{
	    cout << "var() = " << *geneIterator << "\n";
	}

	cout << "\n";
	cout << "\n";
	cout << "Best fitness = " << population[population.size()-1].getFitness() << "\n";//[POPSIZE].fitness << "\n";

  

//
//  Terminate.
//
  cout << "\n";
  cout << "SIMPLE_GA:\n";
  cout << "  Normal end of execution.\n";

  cout << "\n";

  return 0;
}
//****************************************************************************80

void GeneticAlgorithm::crossover ( )

//****************************************************************************80
// 
//  Purpose:
//
//    CROSSOVER selects two parents for the single point crossover.
//
//  Modified:
//
//    29 December 2007
//
//  Local parameters:
//
//    Local, int FIRST, is a count of the number of members chosen.
//
{
	int one, two, three;
	int numParents = 0;
	double x;

	one = -1;
	two = -1;
	three = -1;

  for (int mem = 0; mem < POPSIZE; ++mem )
  {
    x = ( rand ( ) % 1000 ) / 1000.0;
	cout<< numParents << "\n";
    if ( x < PXOVER )
    {
      ++numParents;

      if ( numParents % 4 == 0 )
      {
		  cout<< one<< "\n"<<two<< "\n"<<three<< "\n"<< "\n"<< "\n";
        Xover ( one, two, three, mem);
		one = -1;
		two = -1;
		three = -1;
      }
      else
      {
		  if(one < 0)
			  one = mem;
		  else if(two < 0)
			  two = mem;
		  else
			  three = mem;
      }

    }
  }
  return;
}
//****************************************************************************80

void GeneticAlgorithm::elitist ( )

//****************************************************************************80
// 
//  Purpose:
//
//    ELITIST stores the best member of the previous generation.
//
//  Discussion:
//
//    The best member of the previous generation is stored as 
//    the last in the array. If the best member of the current 
//    generation is worse then the best member of the previous 
//    generation, the latter one would replace the worst member 
//    of the current population.
//
//  Modified:
//
//    29 December 2007
//
//  Local parameters:
//
//    Local, double BEST, the best fitness value.
//
//    Local, double WORST, the worst fitness value.
//
{/*
  int i;
  double best;
  int best_mem;
  double worst;
  int worst_mem;

  best = population[0].fitness;
  worst = population[0].fitness;

  for ( i = 0; i < POPSIZE - 1; ++i )
  {
    if ( population[i].fitness > population[i+1].fitness )
    {

      if ( best <= population[i].fitness )
      {
        best = population[i].fitness;
        best_mem = i;
      }

      if ( population[i+1].fitness <= worst )
      {
        worst = population[i+1].fitness;
        worst_mem = i + 1;
      }

    }
    else
    {

      if ( population[i].fitness <= worst )
      {
        worst = population[i].fitness;
        worst_mem = i;
      }

      if ( best <= population[i+1].fitness )
      {
        best = population[i+1].fitness;
        best_mem = i + 1;
      }

    }

  }
// 
//  If the best individual from the new population is better than 
//  the best individual from the previous population, then 
//  copy the best from the new population; else replace the 
//  worst individual from the current population with the 
//  best one from the previous generation                     
//
  if ( best >= population[POPSIZE].fitness )
  {
    for ( i = 0; i < NVARS; i++ )
    {
      population[POPSIZE].gene[i] = population[best_mem].gene[i];
    }
    population[POPSIZE].fitness = population[best_mem].fitness;
  }
  else
  {
    for ( i = 0; i < NVARS; i++ )
    {
      population[worst_mem].gene[i] = population[POPSIZE].gene[i];
    }
    population[worst_mem].fitness = population[POPSIZE].fitness;
  } 
  */
  return;

}
//****************************************************************************80

void GeneticAlgorithm::evaluate ( )

//****************************************************************************80
// 
//  Purpose:
//
//    EVALUATE implements the user-defined valuation function
//
//  Discussion:
//
//    Each time this is changed, the code has to be recompiled.
//    The current function is:  x[1]^2-x[1]*x[2]+x[3]
//
//  Modified:
//
//    29 December 2007
//
{
  int tools[NTOOLS];
  vector<int>::iterator geneIterator;

  for (int member = 0; member < POPSIZE; ++member )
  {
	  tools[0] = 0;
	  tools[1] = 0;
	  tools[2] = 0;
	  tools[3] = 0;
	  tools[4] = 0;
	  tools[5] = 0;
	  tools[6] = 0;
	for(geneIterator = population[member].getGene().begin(); geneIterator != population[member].getGene().end(); ++geneIterator)
	{

		tools[0] += *geneIterator;
		//cout << " Tool 0::  " << tools[0];
		advance(geneIterator,1);
		tools[1] += *geneIterator;
		//cout << " Tool 1::  " << tools[1];
		advance(geneIterator,1);
		tools[2] += *geneIterator;
		//cout << " Tool 2::  " << tools[2];
		advance(geneIterator,1);
		tools[3] += *geneIterator;
		//cout << " Tool 3::  " << tools[3];
		advance(geneIterator,1);
		tools[4] += *geneIterator;
		//cout << " Tool 4::  " << tools[4];
		advance(geneIterator,1);
		tools[5] += *geneIterator;
		//cout << " Tool 5::  " << tools[5];
		advance(geneIterator,1);
		tools[6] += *geneIterator;
		//cout << "  Tool 6::  " << tools[6] << "\n";
//		if(geneIterator != population[member].gene.end())
//			advance(geneIterator,1);
    }
    population[member].setFitness( ( rangedPercent * tools[0] ) + ( meleePercent * tools[1] ) + ( blockPercent * tools[2] ) 
		+ ( dashPercent * tools[3] ) + ( enemyDPSwing * tools[4] ) + ( enemyDPShot * tools[5] )  );
  }
  return;
}
//****************************************************************************80

void GeneticAlgorithm::initialize ( string file_in_name )

//****************************************************************************80
// 
//  Purpose:
//
//    INITIALIZE initializes the genes within the variables bounds. 
//
//  Discussion:
//
//    It also initializes (to zero) all fitness values for each
//    member of the population. It reads upper and lower bounds 
//    of each variable from the input file `gadata.txt'. It 
//    randomly generates values between these bounds for each 
//    gene of each genotype in the population. The format of 
//    the input file `gadata.txt' is 
//
//      var1_lower_bound var1_upper bound
//      var2_lower_bound var2_upper bound ...
//
//  Modified:
//
//    29 December 2007
//
{
  ifstream file_in;
  //int i;
  int j;

  //int shield;
  //int parry;
  //int acid;
  //int tar;
  //int blade;
  //int projectile;
  //int blob;

  srand (time(0));								//Warning

  file_in.open ( file_in_name.c_str ( ) );

  if ( !file_in )
  {
    cerr << "\n";
    cerr << "Initialize - Fatal error!\n";
    cerr << "  Cannot open the input file!\n";
    exit ( 1 );
  }
// 
//  Initialize variables within the bounds 
//

  population.resize(POPSIZE);

  int current = 0;

  file_in >> rangedPercent >> meleePercent >> blockPercent >> dashPercent >> enemyDPSwing >> enemyDPShot;

  while (!file_in.eof())
  {
    file_in >> current;
	population[0].genePushBack(current);
  }

  //population[POPSIZE].getFitness = 0;

  for ( j = 1; j < POPSIZE; j++ )
  {
    //population[j].setFitness( 0 );
    //population[j].getRFitness( 0 );
    //population[j].getCfitness = 0;
 
	vector<int>::reverse_iterator geneIterator;

	for(geneIterator = population[0].getGene().rbegin(); geneIterator != population[0].getGene().rend(); ++geneIterator)
	{

		population[j].genePushBack(randval(*geneIterator));

    }
  }

  file_in.close ( );

  return;
}
//****************************************************************************80

void GeneticAlgorithm::keep_the_best ( )

//****************************************************************************80
// 
//  Purpose:
//
//    KEEP_THE_BEST keeps track of the best member of the population. 
//
//  Discussion:
//
//    Note that the last entry in the array Population holds a 
//    copy of the best individual.
//
//  Modified:
//
//    29 December 2007
//
//  Local parameters:
//
//    Local, int CUR_BEST, the index of the best individual.
//
{

	std::sort(population.begin(), population.end(), compareFitness);


  /*int currentBest = 0;

  for (int member = 0; member < POPSIZE; member++ )
  {
    if ( population[member].fitness > population[POPSIZE].fitness )
    {
      currentBest = member;
      population[POPSIZE].fitness = population[member].fitness;
    }
  }
// 
//  Once the best member in the population is found, copy the genes.
//

	population[POPSIZE].gene = population[currentBest].gene;
			//cout<< " best" << currentBest;*/
  
  return;
}
//****************************************************************************80

void GeneticAlgorithm::mutate ( )

//****************************************************************************80
// 
//  Purpose:
//
//    MUTATE performs a random uniform mutation. 
//
//  Discussion:
//
//    A variable selected for mutation is replaced by a random value 
//    between the lower and upper bounds of this variable.
//
//  Modified:
//
//    29 December 2007
//
{
  /*double hbound;
  int i;
  int j;
  double lbound;
  double x;

  for ( i = 0; i < POPSIZE; i++ )
  {
    for ( j = 0; j < NVARS; j++ )
    {
      x = rand ( ) % 1000 / 1000.0;
// 
//  Find the bounds on the variable to be mutated 
//
      if ( x < PMUTATION )
      {
        lbound = population[i].lower[j];
        hbound = population[i].upper[j];  
        population[i].gene[j] = randval ( lbound, hbound );
      }
    }
  }
  */
  return;
}
//****************************************************************************80

void GeneticAlgorithm::r8_swap ( int *x, int *y, int *w, int *z )

//****************************************************************************80
// 
//  Purpose:
//
//    R8_SWAP swaps two R8's. 
//
//  Modified:
//
//    29 December 2007
//
{
  int temp;

  temp = *x;
  *x = *y;
  *y = temp;


  /*		point[0][0] = ( rand ( ) % ( population[one].gene.size() - 3 ) );

		if(point[0][0] % NTOOLS < 1)
			point[1][1] = NTOOLS;
		else if(point[0][0] % NTOOLS > 6)
			point[1][1] = -NTOOLS;
		point[1][1] += (point [0][0] % NTOOLS) +
						NTOOLS * ( rand ( ) % ( population[two].gene.size()) / NTOOLS ) ;

		point[2][2] = (point [1][1] / NTOOLS) +
						( rand ( ) % ( ( population[three].gene.size() - 1 ) / NTOOLS) ) ;




		point[0][1] = point[0][0] + 
						( rand ( ) % (population[one].gene.size() - 1 - 2 - point[0][0]) ) + 1;
		
		point[1][2] = (point [0][1]) +
						( rand ( ) % ( population[two].gene.size() / NTOOLS ) ) ;

		point[3][0] = (point [3][0]) +
						( rand ( ) % ( population[four].gene.size() - 1 ) / NTOOLS ) ;




		point[0][2] = ( rand ( ) % ( population[one].gene.size() - 1 - 1) ) + 1;

		point[2][0] = (point [0][2]) +
						( rand ( ) % ( population[three].gene.size() - 1 ) / NTOOLS ) ;

		point[3][1] = (point [2][0]) +
						( rand ( ) % ( population[four].gene.size() - 1 ) / NTOOLS ) ;




		point[1][0] = ( rand ( ) % ( population[two].gene.size() - 1 - 3) ) + 1;

		point[2][1] = (point [1][0]) +
						( rand ( ) % ( population[three].gene.size() - 1 ) / NTOOLS ) ;

		point[3][2] = (point [2][1]) +
						( rand ( ) % ( population[four].gene.size() - 1 ) / NTOOLS ) ;
						*/



  return;
}
//****************************************************************************80

int GeneticAlgorithm::randval ( int x )

//****************************************************************************80
// 
//  Purpose:
//
//    RANDVAL generates a random value within bounds.
//
//  Modified:
//
//    29 December 2007
//
{
  int val;

  val = ( ( rand() % 1000 ) / 1000.0 ) * 2 + (std::abs(x - 1));			//Warning

  return val;
}
//****************************************************************************80

void GeneticAlgorithm::report ( int generation )

//****************************************************************************80
// 
//  Purpose:
//
//    REPORT reports progress of the simulation. 
//
//  Modified:
//
//    29 December 2007
//
//  Local parameters:
//
//    Local, double avg, the average population fitness.
//
//    Local, best_val, the best population fitness.
//
//    Local, double square_sum, square of sum for std calc.
//
//    Local, double stddev, standard deviation of population fitness.
//
//    Local, double sum, the total population fitness.
//
//    Local, double sum_square, sum of squares for std calc.
//
{
  /*double avg;
  double best_val;
  int i;
  double square_sum;
  double stddev;
  double sum;
  double sum_square;

  sum = 0.0;
  sum_square = 0.0;

  for ( i = 0; i < POPSIZE; i++ )
  {
    sum = sum + population[i].fitness;
    sum_square = sum_square + population[i].fitness * population[i].fitness;
  }

  avg = sum / ( double ) POPSIZE;
  square_sum = avg * avg * POPSIZE;
  stddev = sqrt ( ( sum_square - square_sum ) / ( POPSIZE - 1 ) );
  best_val = population[POPSIZE].fitness;

  cout << "  " << setw(8) << generation 
       << " " << best_val 
       << " " << avg 
       << " " << stddev << "\n";
	   */
  return;
}
//****************************************************************************80

void GeneticAlgorithm::selector ( )

//****************************************************************************80
// 
//  Purpose:
//
//    SELECTOR is the selection function.
//
//  Discussion:
//
//    Standard proportional selection for
//    maximization problems incorporating elitist model - makes
//    sure that the best member survives
//
//  Modified:
//
//    29 December 2007
//
{
  int i;
  int j;
  int member;
  double p;
  double sum = 0;

//
//	Order the current population by fitness
//

  std::sort(population.begin(), population.end(), compareFitness);

//
//  Find total fitness of the population 
//
  for ( member = 0; member < POPSIZE; member++ )
  {
    sum = sum + population[member].getFitness();
  }
//
//  Calculate the relative fitness.
//
  for ( member = 0; member < POPSIZE; member++ )
  {
	  population[member].setRFitness( population[member].getFitness() / sum );
  }
  population[0].setCFitness( population[0].getRFitness() );
// 
//  Calculate the cumulative fitness.
//
  for ( member = 1; member < POPSIZE; member++ )
  {
    population[member].setCFitness( population[member-1].getCFitness() +       
      population[member].getRFitness() );
  }
// 
//  Select survivors using cumulative fitness. 
//
  for ( i = 0; i < POPSIZE; i++ )
  { 
    p = rand() % 1000 / 1000.0;
    if (p < population[0].getCFitness())
    {
      newpopulation[i] = population[0];      
    }
    else
    {
      for ( j = 0; j < POPSIZE; j++ )
      { 
        if ( p >= population[j].getCFitness() && p < population[j+1].getCFitness() )
        {
          newpopulation[i] = population[j+1];
        }
      }
    }
  }
// 
//  Once a new population is created, copy it back 
//
  for ( i = 0; i < POPSIZE; i++ )
  {
    population[i] = newpopulation[i]; 
  }
  
  return;     
}
//****************************************************************************80

void GeneticAlgorithm::Xover ( int one, int two, int three, int four )

//****************************************************************************80
// 
//  Purpose:
//
//    XOVER performs crossover of the two selected parents. 
//
//  Modified:
//
//    29 December 2007
//
//  Local parameters:
//
//    Local, int point, the crossover point.
//
{
	/*int point[4][3]; // parent X crossover point
// 
//  Select the crossover point.
//
	cout<<one<<two<< point[0][0]<< "\n";
	point[0][0] = ( rand ( ) % ( population[one].gene.size() - 3 ) ) + 1;
	cout<< point[0][0]<< "\n";

	point[1][0] = (point [0][0] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( population[two].gene.size() / NTOOLS) ) ;
	cout<< point[0][0]<< "\n";
	point[2][0] = (point [0][0] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( population[three].gene.size() / NTOOLS) ) ;
	cout<< point[0][0]<< "\n";
	point[3][0] = (point [0][0] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( population[four].gene.size() / NTOOLS) ) ;
	cout<< point[0][0]<< "\n";

	//point[0][1]
	point[0][1] = (point[0][0]) + 
					( rand ( ) % ( population[one].gene.size() - 2 - point[0][0]) ) + 1;
	cout<< point[0][0]<< "\n";
	//point[1][1]
	if(point[1][0] > (point[0][1] % NTOOLS) )
		point[1][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[two].gene.size() / NTOOLS) 
					- (point[1][0] / NTOOLS) - 1) 
					+ (point[1][0] / NTOOLS) + 1) ;
	else
		point[1][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[two].gene.size() / NTOOLS) 
					- (point[1][0] / NTOOLS) ) 
					+ point[1][0] / NTOOLS) ;
	cout<< point[0][0]<< "\n";
	//point[2][1]
	if(point[2][0] > (point[0][1] % NTOOLS) )
		point[2][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[three].gene.size() / NTOOLS) 
					- (point[2][0] / NTOOLS) - 1) 
					+ (point[2][0] / NTOOLS) + 1) ;
	else
		point[2][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[three].gene.size() / NTOOLS) 
					- (point[2][0] / NTOOLS) ) 
					+ point[2][0] / NTOOLS ) ;

	//point[3][1]
	if(point[3][0] > (point[0][1] % NTOOLS) )
		point[3][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[four].gene.size() / NTOOLS) 
					- (point[3][0] / NTOOLS) - 1) 
					+ (point[3][0] / NTOOLS) + 1) ;
	else
		point[3][1] = (point [0][1] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[four].gene.size() / NTOOLS) 
					- (point[3][0] / NTOOLS) ) + point[3][0] / NTOOLS ) ;


	//point[0][2]
	point[0][2] = (point[0][1]) +
					( rand ( ) % ( population[one].gene.size() - 1 ) ) + 1;
	//point[1][2]
	if(point[1][1] > (point[0][2] % NTOOLS) )
		point[1][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[two].gene.size() / NTOOLS) 
					- (point[1][1] / NTOOLS) - 1) + (point[1][1] / NTOOLS) + 1) ;
	else
		point[1][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[two].gene.size() / NTOOLS) 
					- (point[1][1] / NTOOLS) ) + point[1][1] / NTOOLS ) ;


	//point[2][2]
	if(point[2][1] > (point[0][2] % NTOOLS) )
		point[2][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[three].gene.size() / NTOOLS) 
					- (point[2][1] / NTOOLS) - 1) + (point[2][1] / NTOOLS) + 1) ;
	else
		point[2][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[three].gene.size() / NTOOLS) 
					- (point[2][1] / NTOOLS) ) + point[2][1] / NTOOLS ) ;

	//point[3][2]
	if(point[3][1] > (point[0][2] % NTOOLS) )
		point[3][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[four].gene.size() / NTOOLS) 
					- (point[3][1] / NTOOLS) - 1) + (point[3][1] / NTOOLS) + 1) ;
	else
		point[3][2] = (point [0][2] % NTOOLS) +
					NTOOLS * ( rand ( ) % ( (population[four].gene.size() / NTOOLS) 
					- (point[3][1] / NTOOLS) ) + point[3][1] / NTOOLS ) ;


    for (int i = 0; i < 4; i++ )
    {
		struct genotype g;

		g.fitness = 0;
		g.cfitness = 0;
		g.rfitness = 0;

		std::list<int>::iterator beginIterator;
		std::list<int>::iterator endIterator;

		//Part 1 of new gene
		endIterator = population[one].gene.begin();
		std::advance(endIterator, point[0][0] + 1);
		for(beginIterator = population[one].gene.begin(); 
			beginIterator != endIterator; 
			++beginIterator)
		{
			g.gene.push_back(*beginIterator);
		}

		//Part 2
		beginIterator = population[two].gene.begin();
		endIterator = population[two].gene.begin();
		std::advance(beginIterator, point[1][0]);
		std::advance(endIterator, point[1][1] + 1);
		for(; 
			beginIterator != endIterator; 
			++beginIterator)
		{
			g.gene.push_back(*beginIterator);
		}
		
		//Part 3
		beginIterator = population[three].gene.begin();
		endIterator = population[three].gene.begin();
		std::advance(beginIterator, point[2][1]);
		std::advance(endIterator, point[2][2] + 1);
		for(; 
			beginIterator != endIterator; 
			++beginIterator)
		{
			g.gene.push_back(*beginIterator);
		}

		//Part 4
		beginIterator = population[four].gene.begin();
		std::advance(beginIterator, point[3][2]);
		for(; 
			beginIterator != population[four].gene.end(); 
			++beginIterator)
		{
			g.gene.push_back(*beginIterator);
		}

		population.push_back(g);
      //r8_swap ( &population[one].gene[i], &population[two].gene[i] );
    }
	cout<< "\n"<< "\n"<< "\n"<< "\n"<< "\n";*/
  return;
}