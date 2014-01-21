// Darwin.h

#pragma once

#include <cstdlib>
#include <iostream>
#include <iomanip>
#include <fstream>
#include <cmath>
#include <ctime>
#include <cstring>
#include <list>
#include <iterator>
#include <math.h>
#include <vector>
#include <algorithm>
#include "Genotype.h"

using std::ifstream;
using std::cout;
using std::cerr;
using std::setw;
using std::cin;
using std::endl;
using std::string;
using std::iterator;
using std::list;

namespace DarwinCharles {

	class GeneticAlgorithm
	{
		#define POPSIZE 30
		#define MAXGENS 10
		#define NTOOLS 7
		#define PXOVER 0.8
		#define PMUTATION 0.15
		//
		//  Each GENOTYPE is a member of the population, with
		//  gene: a string of variables,
		//  fitness: the fitness
		//  upper: the variable upper bounds,
		//  lower: the variable lower bounds,
		//  rfitness: the relative fitness,
		//  cfitness: the cumulative fitness.
		//
		/*struct genotype
		{
		  std::list<int> gene;
		  double fitness;
		  //double upper[NVARS];
		  //double lower[NVARS];
		  double rfitness;
		  double cfitness;
		};*/

		struct {
			bool operator()(Genotype &a, Genotype &b)
			{   
			    return a.getFitness() < b.getFitness();
			}   
		} compareFitness;

		std::vector<Genotype> population;//[POPSIZE+1];
		Genotype newpopulation[POPSIZE+1];

		double rangedPercent;
		double meleePercent;
		double blockPercent;
		double dashPercent;
		double enemyDPSwing;
		double enemyDPShot;

	public:
		int run();

		void crossover ( );
		void elitist ( );
		void evaluate ( );
		void initialize ( string file_in_name );
		void keep_the_best ( );
		void mutate ( );
		void r8_swap ( int *, int *, int*, int* );
		int randval ( int );
		void report ( int generation );
		void selector ( );
		void Xover ( int, int, int, int );

	
	};
}