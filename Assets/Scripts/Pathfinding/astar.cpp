#include<vector>
#include<queue>
#include<tuple>
#include<iostream>

using namespace std;
#define WALL '#'

typedef struct node_{
  int x;
  int y;
  int fValue;
  int gValue;
}node;

struct CompareNode {
    bool operator()(node const& n1, node const& n2) {
        return n1.fValue < n2.fValue;
    }
};

int moves[4][2] = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};

int CalculateHValue(int i, int j, pair<int, int> target) {
    return ((i - target.first) + (j - target.second));
}

bool CheckValicMovement(int i, int j,const vector<vector<char>>& grid, vector<vector<bool>>& visited) {
    if (i < 0 || i >= grid.size() || j < 0 || j >= grid[0].size() || visited[i][j] || grid[i][j] == WALL)
        return false;
    return true;
}

bool astar(const vector<vector<char>>& grid, 
         const pair<int, int> target,
         vector<vector<bool>>& visited, 
         queue<pair<int,int>>& searchTree,
         priority_queue<node, vector<node>, CompareNode>& nextPos,
         node curr) {

    visited[curr.x][curr.y] = true;

    searchTree.push(make_pair(curr.x, curr.y));
    
    if(curr.x == target.first && curr.y == target.second)
        return true;
    
    node aux;
    for(int m = 0; m < 4; m++) {
        if (CheckValicMovement(curr.x+moves[m][0], curr.y+moves[m][1], grid, visited)) {
            aux.x = curr.x+moves[m][0];
            aux.y = curr.y+moves[m][1];
            aux.gValue = curr.gValue + 1;
            aux.fValue = aux.gValue + CalculateHValue(curr.x+moves[m][0], curr.y+moves[m][1], target);

            nextPos.push(aux);
        }
    }
    aux = nextPos.top();
    nextPos.pop();
    if(astar(grid, target, visited, searchTree, nextPos, aux))
        return true;

    return(false);
}


int main(){
    int n, m;
    cout << "a";
    cin >> n >> m;

    vector<vector<char>> grid(n, vector<char>(m));
    vector<vector<bool>> visited(n, vector<bool>(m, false));
    priority_queue<node, vector<node>, CompareNode> nextPos;

    node startPos;
    pair<int, int> targetPos;

    for (int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++){
            cin >> grid[i][j];

            if(grid[i][j] == 's'){
                startPos.x = i;
                startPos.y = j;
                startPos.gValue = 0;
            } 

            if(grid[i][j] == 't'){
                targetPos = make_pair(i, j);
            } 
        }

    }

    queue<pair<int,int>> searchTree;

    if(astar(grid, targetPos, visited, searchTree, nextPos, startPos)){
        while (!searchTree.empty())
        {
            pair<int, int> pos = searchTree.front();
            searchTree.pop();
            cout << pos.first << " " << pos.second << endl;
        }
    }

    return 0;
}